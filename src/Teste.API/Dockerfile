# Etapa 1: Construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia arquivos do projeto e restaura dependências
COPY *.sln ./
COPY src/ ./src/
RUN dotnet restore

# Compila a aplicação
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia arquivos compilados para o container final
COPY --from=build /app/out .

# Expor a porta 8080 para acesso externo
EXPOSE 8080

# Define variáveis para permitir conexões externas
ENV ASPNETCORE_URLS=http://+:8080

# Comando para rodar a API
CMD ["dotnet", "MinhaApi.dll"]
