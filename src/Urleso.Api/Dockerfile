﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Directory.Build.props", ""]
COPY ["src/Directory.Packages.props", ""]
COPY ["src/Urleso.Api/Urleso.Api.csproj", "Urleso.Api/"]
COPY ["src/Urleso.Application/Urleso.Application.csproj", "Urleso.Application/"]
COPY ["src/Urleso.Domain/Urleso.Domain.csproj", "Urleso.Domain/"]
COPY ["src/Urleso.Infrastructure/Urleso.Infrastructure.csproj", "Urleso.Infrastructure/"]
COPY ["src/Urleso.Persistence/Urleso.Persistence.csproj", "Urleso.Persistence/"]
COPY ["src/Urleso.Presentation/Urleso.Presentation.csproj", "Urleso.Presentation/"]
RUN dotnet restore "Urleso.Api/Urleso.Api.csproj"
COPY src/ .
WORKDIR "/src/Urleso.Api"
RUN dotnet build "Urleso.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Urleso.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Urleso.Api.dll"]