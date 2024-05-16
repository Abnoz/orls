




FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base  
WORKDIR /app
EXPOSE 8080
EXPOSE 443  
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
WORKDIR /src
COPY ["Silos/Silos.csproj", "Silos/"]
COPY ["Grains/Grains.csproj", "Grains/"]
COPY ["Abstraction/Abstraction.csproj", "Abstraction/"]
 
RUN dotnet restore "Softylines.OrleansApp.Silo/Softylines.OrleansApp.Silo.csproj"
COPY . .
WORKDIR "/src/Softylines.OrleansApp.Silo"
RUN dotnet build "Silos.csproj" -c  Release -o /app/build

FROM build AS publish 
RUN dotnet publish "Silos.csproj" -c Release -o /app/publish  

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Silos.dll"]
