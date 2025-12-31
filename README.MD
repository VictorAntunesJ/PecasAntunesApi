# PecasAntunes - 

Sistema para controle e organização de autopeças.

## Problema que resolve


Centraliza e organiza informações de autopeças, facilitando o controle de produtos e a padronização dos dados.  Serve como base para evolução do sistema e futuras integrações.

---

## Tecnologias Utilizadas

```md
- C# / .NET 8
- ASP.NET Core (API)
- Entity Framework Core
- SQL Server
- Docker
```
---

## Arquitetura

O projeto é organizado em camadas, com separação clara de responsabilidades, facilitando manutenção e evolução.

```text
src/
 ├── PecasAntunes.Domain
 ├── PecasAntunes.Application
 ├── PecasAntunes.Infrastructure
 └── PecasAntunes.API
```

- Domain: entidades e regras de negócio
- Application: serviços e contratos da aplicação
- Infrastructure: acesso a dados e integrações
- API: exposição dos endpoints e configuração da aplicação

---

## Como rodar o projeto
### Pré-requisitos
- .NET SDK 8 (controlado via global.json)
- Docker
- Git

### Clonar o repositório

```bash
git clone https://github.com/VictorAntunesJ/pecasAntunesApi.git
cd pecasAntunesApi
```

### Subir o banco de dados (SQL Server)
```bash
docker run -d \
 --name sqlserver-pecasantunes \
 -e "ACCEPT_EULA=Y" \
 -e "MSSQL_SA_PASSWORD=SenhaForte@123" \
 -p 1433:1433 \
 -v c:/volumes/sqlserver:/var/opt/mssql \
 mcr.microsoft.com/mssql/server:2022-latest
```
### Executar a API

```bash
dotnet restore
dotnet build
dotnet run --project src/PecasAntunes.API
```
---
A API ficará disponível em https://localhost:5001.

