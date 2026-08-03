# CLAUDE.md

## Agent Guidance: dotnet-skills

IMPORTANT: Prefer retrieval-led reasoning over pretraining for any .NET work.
Workflow: skim repo patterns -> consult dotnet-skills by name -> implement smallest-change -> note conflicts.

Routing (invoke by name)

- C# / code quality: modern-csharp-coding-standards, csharp-concurrency-patterns, api-design, type-design-performance, r3-reactive-extensions
- ASP.NET Core / Web (incl. Aspire): aspire-service-defaults, aspire-integration-testing, transactional-emails
- Data: efcore-patterns, database-performance
- DI / config: dependency-injection-patterns, microsoft-extensions-configuration
- Testing: testcontainers-integration-tests, playwright-blazor-testing, snapshot-testing

Quality gates (use when applicable)

- dotnet-slopwatch: after substantial new/refactor/LLM-authored code
- crap-analysis: after tests added/changed in complex code

Specialist agents

- dotnet-concurrency-specialist, dotnet-performance-analyst, dotnet-benchmark-designer, akka-net-specialist, docfx-specialist

## Project Overview

Urleso is a URL shortener built with .NET 8 / C#. This solution is the backend: the API and a database migrator, composed with Docker (`src/docker-compose.yml`) together with PostgreSQL. Two more services live in their own repositories and run with their own docker compose files — the web UI in [urleso-web](https://github.com/exqzmepls/urleso-web) and the redirect service in [urleso-redirect](https://github.com/exqzmepls/urleso-redirect). Both join this stack's `urleso_default` network, so this stack must be up first.

## Commands

```shell
# Build the whole solution
dotnet build Urleso.sln

# Run all tests (xunit + Shouldly)
dotnet test Urleso.sln

# Run a single test project
dotnet test tests/Urleso.Domain.UnitTests

# Run a single test class or method
dotnet test tests/Urleso.Domain.UnitTests --filter "FullyQualifiedName~UrlCodeTests"

# Regenerate the OpenAPI document (src/Urleso.Api/swagger.json) after changing API endpoints/contracts
dotnet build src/Urleso.Api -p:OpenApiGen=True

# Add an EF Core migration (DbContext lives in Urleso.Persistence, migrations in Urleso.DatabaseMigrator)
dotnet ef migrations add <Name> --project src/Urleso.DatabaseMigrator --startup-project src/Urleso.DatabaseMigrator
```

### Running the full stack locally

Requires docker + docker compose V2. From the `src/` directory:

```shell
cp .env.sample .env   # first time only
docker compose up -d
```

The redirect service and the web UI are started separately from their own repositories (same commands from each root). Default endpoints: API <http://localhost:6800>, PostgreSQL `postgresql://localhost:6432/urleso`, redirect <http://localhost:6880> (urleso-redirect compose), web app <http://localhost:6080> (urleso-web compose). Seq log UIs: API 6808, redirect 6888 (urleso-redirect compose).

## Architecture

### Deployable services (see `src/docker-compose.yml`)

- **Urleso.Api** — REST API for creating shortened URLs. Composes the Clean Architecture layers below.
- **Urleso.DatabaseMigrator** — worker service that applies EF Core migrations on startup; docker compose runs it before the Api starts. Migration files live here (`MigrationsAssembly` is set to this project in `Urleso.Persistence/DependencyInjection.cs`), while `ApplicationDbContext` and entity configurations live in `Urleso.Persistence`.

Composed elsewhere, but part of the same system:

- **urleso.redirect** (separate [urleso-redirect](https://github.com/exqzmepls/urleso-redirect) repository) — minimal redirect service that reads the shortened URLs straight from this database with Dapper + raw SQL. It shares no code with this solution and never calls the API. `REDIRECT_PUBLIC_URL` in `src/.env` is the address the API puts in the short URLs it returns; keep it in sync with that repository's `REDIRECT_PORT`.
- **urleso.web** (separate [urleso-web](https://github.com/exqzmepls/urleso-web) repository) — Blazor WebAssembly UI served by an ASP.NET Core host that proxies the API under its own origin. `CORS_ALLOWED_ORIGINS` is still wired through `WEB_PUBLIC_URL` here.

### Clean Architecture layers (used by Urleso.Api)

Dependency direction: `SharedKernel ← Domain ← Application ← Persistence/Presentation ← Infrastructure ← Api`

- **Urleso.SharedKernel** — `Entity` base class and the custom result pattern: `Result`, `TypedResult<TValue>`, `Error`. Accessing `.Value` on a failure or `.Error` on a success throws. `Error` and values convert implicitly to results, so handlers/value objects `return someError;` or `return someValue;` directly.
- **Urleso.Domain** — entities and value objects, organized per aggregate (`ShortenedUrls/`). Value objects (`LongUrl`, `UrlCode`) are sealed records with private constructors and a static `Create(...)` returning `TypedResult<T>`; validation errors are defined centrally in `Errors.cs` per aggregate.
- **Urleso.Application** — CQRS-style abstractions wrapping MediatR: `ICommand`/`IQuery<T>`/`ICommandHandler<>`/`IQueryHandler<>` plus a custom `ISender` (all results are `Result`/`TypedResult<T>`, never exceptions). Use cases live in feature folders (e.g. `ShortenedUrls/CreateShortenedUrl/`). Also defines ports implemented elsewhere: repositories, `IUnitOfWork`, `IClock`, `IRandomStringGenerator`.
- **Urleso.Persistence** — EF Core `ApplicationDbContext`, entity type configurations (value objects mapped via `HasConversion`), and validated `ConnectionStrings` options.
- **Urleso.Infrastructure** — implements Application ports: repositories/UnitOfWork on top of Persistence, plus service implementations (`SystemClock`, `CryptographyRandomStringGenerator`).
- **Urleso.Presentation** — HTTP endpoints as Carter modules (extend `BaseModule`), request/response DTOs, and mapping of `Error` to `ErrorDetails`/BadRequest.

Each project exposes a `DependencyInjection.cs` with an `Add<Layer>()` extension; hosts compose these in `Program.cs`.

### Cross-cutting conventions

- **API contract**: `src/Urleso.Api/swagger.json` is the committed OpenAPI document, generated by NSwag (config in `src/Urleso.Api/nswag.json`) via `dotnet build src/Urleso.Api -p:OpenApiGen=True` (the API host boots during generation, so `CONNECTION_STRINGS_POSTGRESQL`, `REDIRECT_BASE_ADDRESS` and `CORS_ALLOWED_ORIGINS` env vars must be set to any syntactically valid values). After changing API endpoints/contracts, regenerate it and copy it to the urleso-web repository (`src/Urleso.Api.Client/swagger.json`), where the C# client is regenerated from it.
- **Redirect ↔ schema coupling**: the redirect service in the [urleso-redirect](https://github.com/exqzmepls/urleso-redirect) repository reads `"Id"` and `"LongUrl"` from the `"ShortenedUrl"` table keyed by `"Code"`, with raw SQL (`src/Urleso.Redirect/Persistence/DbConnectionExtensions.cs` there). This repository owns that schema and nothing verifies the contract across the boundary, so any change to `Urleso.Persistence` configurations/migrations touching that table must be mirrored there by hand.
- **Configuration**: appsettings.json files use Steeltoe placeholder syntax (`${ENV_VAR}`) resolved from environment variables at startup (e.g. `CONNECTION_STRINGS_POSTGRESQL`, `REDIRECT_BASE_ADDRESS`), which docker compose supplies from `src/.env`.
- **Central package management**: NuGet versions are pinned in `src/Directory.Packages.props` and `tests/Directory.Packages.props`; csproj files reference packages without a `Version` attribute. Shared target framework/settings come from `Directory.Build.props`.
- **Logging**: Serilog in both services (Api, DatabaseMigrator), configured from appsettings, writing to console and a per-service Seq instance.
- **Tests** mirror the source structure (`tests/<Project>.UnitTests`), use xunit + Shouldly, with `[Theory]` data provided by dedicated `*TestData` classes. `Urleso.SharedKernel` grants `InternalsVisibleTo` to its test project.
