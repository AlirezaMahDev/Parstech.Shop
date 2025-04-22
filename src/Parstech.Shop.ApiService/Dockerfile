FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Parstech.Shop.ApiService/Parstech.Shop.ApiService.csproj", "src/Parstech.Shop.ApiService/"]
COPY ["src/Parstech.Shop.ServiceDefaults/Parstech.Shop.ServiceDefaults.csproj", "src/Parstech.Shop.ServiceDefaults/"]
RUN dotnet restore "src/Parstech.Shop.ApiService/Parstech.Shop.ApiService.csproj"
COPY . .
WORKDIR "/src/src/Parstech.Shop.ApiService"
RUN dotnet build "./Parstech.Shop.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Parstech.Shop.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Parstech.Shop.ApiService.dll"]
