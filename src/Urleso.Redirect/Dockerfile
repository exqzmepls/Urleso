FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["../Directory.Build.props", ""]
COPY ["../Directory.Packages.props", ""]
COPY ["Urleso.Redirect/Urleso.Redirect.csproj", "Urleso.Redirect/"]
RUN dotnet restore "Urleso.Redirect/Urleso.Redirect.csproj"
COPY . .
WORKDIR "/src/Urleso.Redirect"
RUN dotnet build "Urleso.Redirect.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Urleso.Redirect.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Urleso.Redirect.dll"]
