FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /build

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "TravelHub.sln" "TravelHub.sln"

COPY "src/Recipes.API/Recipes.API.csproj" "src/Recipes.API/Recipes.API.csproj"
COPY "src/Trips.API/Trips.API.csproj" "src/Trips.API/Trips.API.csproj"
COPY "src/cHub.ServiceDefaults/cHub.ServiceDefaults.csproj" "src/cHub.ServiceDefaults/cHub.ServiceDefaults.csproj"
COPY "src/WebApp/WebApp.esproj" "src/WebApp/WebApp.esproj"
COPY "NuGet.config" "NuGet.config"
RUN dotnet restore "TravelHub.sln"

COPY . .
WORKDIR /build/src/Trips.API
RUN dotnet publish -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
#COPY --from=build /src/Services/Catalog/Catalog.API/Proto /app/Proto
#COPY --from=build /src/Services/Catalog/Catalog.API/eshop.pfx .
ENTRYPOINT ["dotnet", "Trips.API.dll"]