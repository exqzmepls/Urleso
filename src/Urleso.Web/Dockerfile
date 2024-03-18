FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["../Directory.Build.props", ""]
COPY ["../Directory.Packages.props", ""]
COPY ["Urleso.Web/Urleso.Web.csproj", "Urleso.Web/"]
COPY ["Urleso.Api.Client/Urleso.Api.Client.csproj", "Urleso.Api.Client/"]
RUN dotnet restore "Urleso.Web/Urleso.Web.csproj"
COPY . .
WORKDIR "/src/Urleso.Web"
RUN dotnet build "Urleso.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Urleso.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Urleso.Web.dll"]
