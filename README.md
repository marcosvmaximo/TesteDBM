# Teste.API

## 🚀 Descrição do Projeto
O **Teste.API** é uma API .NET 8 para manipulação de dados com PostgreSQL, containerizada via Docker para fácil execução.

## ⚙Configuração e Execução

### 1. Clone o Repositório
```bash
git clone https://github.com/seu-usuario/teste-api.git
cd teste-api
```


### 2. Configure o Banco de Dados
Rodando localmente ou via Docker:
```bash
docker run --name postgres -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=teste -p 5432:5432 -d postgres:13
```


### 3. Configure a String de Conexão
Edite `appsettings.Development.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=teste"
}
```


### 4. Rode a API
```bash
dotnet restore
dotnet build
dotnet run --project src/Teste.API/Teste.API.csproj
```


API em `http://localhost:8080` 🌐


## Docker

### 1. Criar Imagem
```bash
docker build -t teste-api .
```


### 2. Rodar com Docker Compose
```bash
docker-compose up --build
```

API: `http://localhost:5221` | Banco: `localhost:5432`


### 3. Parar Containers
```bash
docker-compose down
```


### 4. Baixar Imagem do DockerHub
```bash
docker pull seu-usuario/teste-api:latest
docker run -p 8080:80 seu-usuario/teste-api:latest
```


## Executando Migrações

```bash
dotnet ef database update
```

Para criar uma nova:
```bash
dotnet ef migrations add NomeDaMigracao
```

## Rodando Testes

```bash
dotnet test
```
