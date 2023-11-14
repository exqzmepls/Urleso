FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Urleso.Web/Urleso.Web.csproj", "src/Urleso.Web/"]
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
