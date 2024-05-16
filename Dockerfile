﻿FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
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

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["Silos/appsettings.json", "appsettings.json"]
ENTRYPOINT ["dotnet","Silos.dll"]
