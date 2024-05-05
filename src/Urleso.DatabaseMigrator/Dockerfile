FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["../Directory.Build.props", ""]
COPY ["../Directory.Packages.props", ""]
COPY ["Urleso.DatabaseMigrator/Urleso.DatabaseMigrator.csproj", "Urleso.DatabaseMigrator/"]
COPY ["Urleso.Domain/Urleso.Domain.csproj", "Urleso.Domain/"]
COPY ["Urleso.Persistence/Urleso.Persistence.csproj", "Urleso.Persistence/"]
RUN dotnet restore "Urleso.DatabaseMigrator/Urleso.DatabaseMigrator.csproj"
COPY . .
WORKDIR "/src/Urleso.DatabaseMigrator"
RUN dotnet build "Urleso.DatabaseMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Urleso.DatabaseMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Urleso.DatabaseMigrator.dll"]
