FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Directory.Build.props", "src/"]
COPY ["src/Directory.Packages.props", "src/"]
COPY ["src/Urleso.Web/Urleso.Web.csproj", "src/Urleso.Web/"]
COPY ["src/Core/Urleso.Application/Urleso.Application.csproj", "src/Core/Urleso.Application/"]
COPY ["src/Core/Urleso.Domain/Urleso.Domain.csproj", "src/Core/Urleso.Domain/"]
COPY ["src/External/Urleso.Infrastructure/Urleso.Infrastructure.csproj", "src/External/Urleso.Infrastructure/"]
COPY ["src/External/Urleso.Persistence/Urleso.Persistence.csproj", "src/External/Urleso.Persistence/"]
COPY ["src/External/Urleso.Presentation/Urleso.Presentation.csproj", "src/External/Urleso.Presentation/"]
RUN dotnet restore "src/Urleso.Web/Urleso.Web.csproj"
COPY . .
WORKDIR "/src/src/Urleso.Web"
RUN dotnet build "Urleso.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Urleso.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Urleso.Web.dll"]
