FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", ""]
COPY ["Directory.Packages.props", ""]
COPY ["Urleso.Web/Urleso.Web.csproj", "Urleso.Web/"]
COPY ["Urleso.Api.Client/Urleso.Api.Client.csproj", "Urleso.Api.Client/"]
RUN dotnet restore "Urleso.Web/Urleso.Web.csproj"
COPY . .
WORKDIR "/src/Urleso.Web"
RUN dotnet publish "Urleso.Web.csproj" -c Release -o /app/publish

FROM nginx:1.27-alpine AS final
EXPOSE 8080
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY Urleso.Web/nginx.conf /etc/nginx/conf.d/default.conf
COPY --chmod=755 Urleso.Web/docker-entrypoint.d/40-appsettings.sh /docker-entrypoint.d/40-appsettings.sh
