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

Urleso is a URL shortener built with .NET 8 / C#. It is a solution of three deployable services plus a database migrator, all composed with Docker.

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

# Regenerate the API client (Urleso.Api.Client) after changing API endpoints/contracts
dotnet build src/Urleso.Api -p:ClientGen=True

# Add an EF Core migration (DbContext lives in Urleso.Persistence, migrations in Urleso.DatabaseMigrator)
dotnet ef migrations add <Name> --project src/Urleso.DatabaseMigrator --startup-project src/Urleso.DatabaseMigrator
```

### Running the full stack locally

Requires docker + docker compose V2. From the `src/` directory:

```shell
cp .env.sample .env   # first time only
docker compose up -d
```

Default endpoints: web app <http://localhost:6080>, API <http://localhost:6800>, redirect <http://localhost:6880>, PostgreSQL `postgresql://localhost:6432/urleso`. Seq log UIs: API 6808, redirect 6888.

## Architecture

### Deployable services (see `src/docker-compose.yml`)

- **Urleso.Api** — REST API for creating shortened URLs. Composes the Clean Architecture layers below.
- **Urleso.Redirect** — standalone, minimal redirect service. Deliberately does NOT reference Domain/Application; it has its own internal Application/Persistence/Presentation folders and reads the database directly with Dapper + raw SQL for a fast read path.
- **Urleso.Web** — standalone Blazor WebAssembly UI (MudBlazor), served as static files by nginx in Docker. References only `Urleso.Api.Client`; the browser calls the API directly (CORS is configured in `Urleso.Api` via `CORS_ALLOWED_ORIGINS`). Container env vars are invisible to WASM, so `docker-entrypoint.d/40-appsettings.sh` regenerates `wwwroot/appsettings.json` from `API_BASE_ADDRESS` at container start.
- **Urleso.DatabaseMigrator** — worker service that applies EF Core migrations on startup; docker compose runs it before Api/Redirect start. Migration files live here (`MigrationsAssembly` is set to this project in `Urleso.Persistence/DependencyInjection.cs`), while `ApplicationDbContext` and entity configurations live in `Urleso.Persistence`.

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

- **Generated API client**: `Urleso.Api.Client`'s `Client.g.cs`/`Contracts.g.cs` are generated by NSwag from the API (config in `src/Urleso.Api/nswag.json`). Never edit them by hand — change the API and rebuild with `-p:ClientGen=True`.
- **Redirect ↔ schema coupling**: `Urleso.Redirect/Persistence/DbConnectionExtensions.cs` queries the `"ShortenedUrl"` table with raw SQL. Any schema change in `Urleso.Persistence` configurations/migrations must be mirrored there.
- **Configuration**: appsettings.json files use Steeltoe placeholder syntax (`${ENV_VAR}`) resolved from environment variables at startup (e.g. `CONNECTION_STRINGS_POSTGRESQL`, `REDIRECT_BASE_ADDRESS`), which docker compose supplies from `src/.env`.
- **Central package management**: NuGet versions are pinned in `src/Directory.Packages.props` and `tests/Directory.Packages.props`; csproj files reference packages without a `Version` attribute. Shared target framework/settings come from `Directory.Build.props`.
- **Logging**: Serilog in the server services (Api, Redirect, DatabaseMigrator), configured from appsettings, writing to console and a per-service Seq instance. Urleso.Web (WASM) logs to the browser console via the standard `ILogger`.
- **Tests** mirror the source structure (`tests/<Project>.UnitTests`), use xunit + Shouldly, with `[Theory]` data provided by dedicated `*TestData` classes. `Urleso.SharedKernel` grants `InternalsVisibleTo` to its test project.
