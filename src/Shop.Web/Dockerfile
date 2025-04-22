FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Shop.Web/Shop.Web.csproj", "src/Shop.Web/"]
COPY ["src/Shop.Application/Shop.Application.csproj", "src/Shop.Application/"]
COPY ["src/Shop.Domain/Shop.Domain.csproj", "src/Shop.Domain/"]
COPY ["src/Shop.Infrastructure/Shop.Infrastructure.csproj", "src/Shop.Infrastructure/"]
COPY ["src/Shop.Persistence/Shop.Persistence.csproj", "src/Shop.Persistence/"]
COPY ["src/Parstech.Shop.ServiceDefaults/Parstech.Shop.ServiceDefaults.csproj", "src/Parstech.Shop.ServiceDefaults/"]
RUN dotnet restore "src/Shop.Web/Shop.Web.csproj"
COPY . .
WORKDIR "/src/src/Shop.Web"
RUN dotnet build "./Shop.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Shop.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Web.dll"]
