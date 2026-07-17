# 🚗 PecasAntunes API

> API REST para gerenciamento de autopeças desenvolvida com **ASP.NET Core (.NET 8)** aplicando princípios de **Clean Architecture**, boas práticas de desenvolvimento backend e deploy em ambiente de produção.

![.NET](https://img.shields.io/badge/.NET-8-purple)
![C#](https://img.shields.io/badge/C%23-Backend-blue)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-blue)
![Docker](https://img.shields.io/badge/Docker-Containerization-blue)
![License](https://img.shields.io/badge/license-MIT-green)


## 📌 Sobre o projeto

O **PecasAntunes API** é uma Web API REST desenvolvida para gerenciamento de produtos de uma empresa de autopeças.

O projeto foi criado com foco em demonstrar conhecimentos práticos em desenvolvimento backend utilizando o ecossistema **.NET**, incluindo:

- Construção de APIs REST
- Separação de responsabilidades
- Arquitetura limpa
- Persistência de dados
- Validações de negócio
- Documentação de endpoints
- Deploy em ambiente cloud


## 🚀 Demonstração em produção

### 🌐 Aplicação Front-end

A API possui uma interface web simples para consumo dos endpoints:

🔗 https://victorantunesj.github.io/PecasAntunesApi/


### 📚 Documentação Swagger

Acesse a documentação interativa da API:

🔗 https://pecasantunes-api.onrender.com/swagger


### ❤️ Health Check

Endpoint para monitoramento da aplicação:

🔗 https://pecasantunes-api.onrender.com/health


---

# 🏗️ Arquitetura

O projeto utiliza **Clean Architecture**, mantendo separação entre regras de negócio, aplicação, infraestrutura e apresentação.


```
src
│
├── PecasAntunes.Domain
│   ├── Entidades
│   ├── Regras de negócio
│   └── Validações
│
├── PecasAntunes.Application
│   ├── Serviços
│   ├── DTOs
│   └── Casos de uso
│
├── PecasAntunes.Infrastructure
│   ├── Entity Framework Core
│   ├── Repositórios
│   └── Configuração do banco
│
└── PecasAntunes.API
    ├── Controllers
    ├── Middlewares
    └── Configuração da aplicação
```


---

# ⚙️ Funcionalidades

## Gestão de Autopeças

✔ Criar uma nova peça  
✔ Listar peças cadastradas  
✔ Buscar peça por identificação  
✔ Atualizar informações  
✔ Remover registros  


## Recursos adicionais

✔ Validação de regras de negócio  
✔ Tratamento global de exceções  
✔ Injeção de dependência  
✔ Programação assíncrona  
✔ Versionamento da API  
✔ Documentação Swagger/OpenAPI  
✔ Health Check  


---

# 📡 Endpoints principais

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/v1/AutoPecas` | Lista todas as peças |
| GET | `/api/v1/AutoPecas/{id}` | Busca peça por ID |
| POST | `/api/v1/AutoPecas` | Cadastra uma peça |
| PUT | `/api/v1/AutoPecas/{id}` | Atualiza uma peça |
| DELETE | `/api/v1/AutoPecas/{id}` | Remove uma peça |
| GET | `/health` | Verifica status da API |


---

# 🛠️ Tecnologias utilizadas

## Backend

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core


## Banco de dados

- PostgreSQL
- Neon Database


## Arquitetura e qualidade

- Clean Architecture
- Repository Pattern
- Dependency Injection
- DTO Pattern


## Ferramentas

- Swagger/OpenAPI
- Docker
- Git
- GitHub
- Render


---

# 🗄️ Banco de dados

A aplicação utiliza PostgreSQL em produção.

Configuração através de variável de ambiente:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=HOST;Database=DATABASE;Username=USER;Password=PASSWORD"
  }
}
```


---

# ▶️ Executando localmente

## Pré-requisitos

Instalar:

- .NET 8 SDK
- PostgreSQL ou Docker
- Git


## Clonar projeto

```bash
git clone https://github.com/VictorAntunesJ/PecasAntunes.API.git
```


## Restaurar dependências

```bash
dotnet restore
```


## Executar migrations

```bash
dotnet ef database update \
--project src/PecasAntunes.Infrastructure \
--startup-project src/PecasAntunes.API
```


## Executar API

```bash
dotnet run --project src/PecasAntunes.API
```


API disponível:

```
https://localhost:xxxx/swagger
```


---

# 🐳 Executando com Docker

Criar imagem:

```bash
docker build -t pecasantunes-api .
```


Executar container:

```bash
docker run -p 8080:8080 pecasantunes-api
```


---

# ☁️ Deploy

A aplicação está publicada utilizando:

### API
- Render

### Banco
- Neon PostgreSQL

### Front-end
- GitHub Pages


O deploy utiliza:

- Dockerfile
- Variáveis de ambiente
- Configuração para ambiente de produção


---

# 📸 Screenshots

Adicionar imagens:

```
/docs
 ├── swagger.png
 ├── arquitetura.png
 └── sistema.png
```


---

# 👨‍💻 Autor

**Victor Antunes**

Desenvolvedor Back-end .NET

Tecnologias:

C# | .NET | ASP.NET Core | APIs REST | PostgreSQL


LinkedIn:

https://www.linkedin.com/in/victorantunesj/


GitHub:

https://github.com/VictorAntunesJ


---

## 🎯 Objetivo do projeto

Projeto desenvolvido para demonstrar habilidades práticas em desenvolvimento backend, arquitetura de software, banco de dados, APIs REST e publicação de aplicações em ambiente real.


