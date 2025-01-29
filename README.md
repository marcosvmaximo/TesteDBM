# Descrição do Projeto
API para o teste da DBM, utilizando **.NET 8**, **Docker** e **banco de dados em memória**.

---

## Configuração e Execução

### Clone o Repositório  
```sh
 git clone https://github.com/seu-usuario/teste-api.git
 cd teste-api
```

### Executando a API

#### Rodando Localmente  
```sh
 dotnet restore
 dotnet build
 dotnet run --project src/Teste.API/Teste.API.csproj
```
A API estará disponível em:  
**http://localhost:8080**

Utilizando o Swagger:
**http://localhost:8080/swagger/index.html**

---

## Execução com Docker

### Utilizar a Imagem do Docker Hub  
```sh
 docker pull seu-usuario/teste-api:latest
 docker run -p 8080:8080 seu-usuario/teste-api:latest
```
A API estará acessível em:  
**http://localhost:8080**

Utilizando o Swagger:
**http://localhost:8080/swagger/index.html**

### Parar Containers  
```sh
 docker stop $(docker ps -q)
 docker rm $(docker ps -aq)
```

---

## Configuração do Banco de Dados
A API já está configurada para usar um **banco de dados em memória**, então não é necessário configurar um banco externo, porém está pre programado para rodar utilizando o Postgresql.

No `Program.cs`, o banco está definido como InMemory:
```csharp
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("MinhaMemoriaDB"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
```

---

## Executando Testes
```sh
 dotnet test
```

# Documentação

## 1. Estrutura do Projeto
Opetei por utilizar uma arquitetura de 4 camadas, seguindo alguns conceitos de DDD.

```
Teste.API/
│-- src/
│   │-- Teste.API/           
│   │-- Teste.Aplication/           
│   │-- Teste.Domain/           
│   │-- Teste.Infra/           
│   │-- Teste.API.Tests/
```

## 2. Descrição das Camadas e Responsabilidades

1. API (Camada de Apresentação)
Responsável por expor os endpoints REST.
Contém os Controllers, que recebem requisições e retornam respostas.
Converte as requisições em chamadas para a camada Application.

2. Application (Camada de Aplicação)
Contém os Casos de Uso (Use Cases) e Serviços que coordenam a lógica de negócio.
Converte dados entre DTOs e modelos do domínio.
Faz chamadas para a camada Domain para executar regras de negócio.
Implementa regras e validações da aplicação.

4. Domain (Camada de Domínio)
Contém as Entidades e Interfaces que definem as regras de negócio.
Independente de frameworks ou banco de dados.

5. Infra (Camada de Infraestrutura)
Gerencia a persistência de dados e a comunicação com serviços externos.
Contém implementações de Repositories que acessam o banco usando Entity Framework.

## 3. Escolha de Tecnologias e Padrões de Projeto

A stack principal do projeto inclui:

- **.NET 8**:
- **InMemoryDatabase**: Banco de dados de teste.
- **Entity Framework Core**
- **Docker**
- **Arquitetura em Camadas**
- **Repository Pattern**

## 4. Desafios Encontrados e Soluções

- **Gerenciamento de Conexões com Banco de Dados**: Tive alguns imprevisto para conectar com o banco de dados na contarinização da aplicação.
- **Migrações de Banco de Dados**: Na utilização do FluentMigrator, por isso tive que mesclar com o conceito de criação do banco de dados em memória.
- **Testabilidade**: Uso de injeção de dependência para facilitar mocks nos testes.

## 5. Plano de Testes

Os testes unitários cobrem os seguintes cenários:

- **Testes de Services e Repositórios**: Valida operações dos serviços e repositório da aplicação.
- **Testes de Validação das Entidade**: Valida a lógica de criação da entidade Produto.


Os testes são executados com:
```bash
dotnet test
```
