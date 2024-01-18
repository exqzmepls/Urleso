FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Directory.Build.props", ""]
COPY ["src/Directory.Packages.props", ""]
COPY ["src/Urleso.DatabaseMigrator/Urleso.DatabaseMigrator.csproj", "Urleso.DatabaseMigrator/"]
COPY ["src/Urleso.Domain/Urleso.Domain.csproj", "Urleso.Domain/"]
COPY ["src/Urleso.Persistence/Urleso.Persistence.csproj", "Urleso.Persistence/"]
RUN dotnet restore "src/Urleso.DatabaseMigrator/Urleso.DatabaseMigrator.csproj"
COPY src/ .
WORKDIR "/src/Urleso.DatabaseMigrator"
RUN dotnet build "Urleso.DatabaseMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Urleso.DatabaseMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Urleso.DatabaseMigrator.dll"]
