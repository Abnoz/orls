FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Silos/Silos.csproj", "Silos/"]
COPY ["Grains/Grains.csproj", "Grains/"]
COPY ["Abstraction/Abstraction.csproj", "Abstraction/"]
RUN dotnet restore "Silos/Silos.csproj"
COPY . .
WORKDIR "/src/Silos"
RUN dotnet build "Silos.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Silos.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Silos.dll"]
