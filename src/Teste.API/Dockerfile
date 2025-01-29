# Estágio base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Teste.API/Teste.API.csproj", "src/Teste.API/"]
COPY ["src/Teste.Domain/Teste.Domain.csproj", "src/Teste.Domain/"]
COPY ["src/Teste.Core/Teste.Core.csproj", "src/Teste.Core/"]
COPY ["src/Teste.Infra/Teste.Infra.csproj", "src/Teste.Infra/"]
COPY ["src/Teste.Application/Teste.Application.csproj", "src/Teste.Application/"]
RUN dotnet restore "src/Teste.API/Teste.API.csproj"
COPY . .
WORKDIR "/src/src/Teste.API"
RUN dotnet build "Teste.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação da aplicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Teste.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Estágio final para execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Rodar migrações antes de iniciar a API
RUN dotnet ef database update --no-build --connection "Host=postgres;Port=5432;Username=postgres;Password=postgres;Database=postgres"

ENTRYPOINT ["dotnet", "Teste.API.dll"]