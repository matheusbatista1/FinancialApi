# FinancialApi

FinancialApi é uma API RESTful desenvolvida em .NET 8 para gerenciar operações financeiras, incluindo cadastro de pessoas, contas, cartões e transações. Utiliza um banco de dados PostgreSQL e autenticação JWT para segurança. Este projeto foi construído com foco em modularidade, utilizando o padrão de arquitetura limpa com camadas de aplicação, domínio e infraestrutura.

## Tecnologias Utilizadas

- **Backend**: .NET 8, ASP.NET Core
- **Banco de Dados**: PostgreSQL 16
- **Autenticação**: JWT (JSON Web Tokens)
- **Dependências**: MediatR, FluentValidation, Entity Framework Core, Refit
- **Containerização**: Docker
- **Testes**: xUnit, Moq, FluentAssertions (testes unitários implementados)

## Funcionalidades

- Cadastro e gerenciamento de pessoas (CPF/CNPJ).
- Criação e consulta de contas bancárias.
- Gerenciamento de cartões associados às contas.
- Registro de transações financeiras.
- Autenticação via login com geração de token JWT.
- Integração com uma API externa de compliance para validação de documentos.

## Estrutura do Projeto

- `FinancialApi.Domain`: Entidades e interfaces do domínio.
- `FinancialApi.Application`: Lógica de aplicação, comandos e validações.
- `FinancialApi.Infrastructure`: Implementações de repositórios, serviços e configuração do banco de dados.
- `FinancialApi.API`: Camada de apresentação com controllers e configuração da API.
- `FinancialApi.Tests`: Testes unitários (testes de integração estão em desenvolvimento).

## Rotas Disponíveis

A API utiliza o padrão REST e suporta os seguintes endpoints (acessíveis via Swagger em `/swagger` em ambiente de desenvolvimento):

### Pessoas
- `POST /api/people`  
  - **Descrição**: Cria uma nova pessoa (valida CPF ou CNPJ).  
  - **Corpo da Requisição**: `{ "name": "string", "document": "string", "password": "string" }`  
  - **Resposta**: `201 Created` com os dados da pessoa criada.  
  - **Autenticação**: Não requerida.

### Contas
- `POST /api/accounts`  
  - **Descrição**: Cria uma nova conta bancária para uma pessoa autenticada.  
  - **Corpo da Requisição**: `{ "personId": "guid" }`  
  - **Resposta**: `201 Created` com os dados da conta.  
  - **Autenticação**: Requer token JWT.
- `GET /api/accounts`  
  - **Descrição**: Lista todas as contas do usuário autenticado.  
  - **Resposta**: `200 OK` com a lista de contas.  
  - **Autenticação**: Requer token JWT.

### Cartões
- `POST /api/cards`  
  - **Descrição**: Cria um novo cartão associado a uma conta.  
  - **Corpo da Requisição**: `{ "accountId": "guid" }`  
  - **Resposta**: `201 Created` com os dados do cartão.  
  - **Autenticação**: Requer token JWT.

### Transações
- `POST /api/transactions`  
  - **Descrição**: Registra uma nova transação em uma conta.  
  - **Corpo da Requisição**: `{ "accountId": "guid", "amount": "decimal", "description": "string" }`  
  - **Resposta**: `201 Created` com os dados da transação.  
  - **Autenticação**: Requer token JWT.

### Autenticação
- `POST /api/auth/login`  
  - **Descrição**: Autentica um usuário e retorna um token JWT.  
  - **Corpo da Requisição**: `{ "document": "string", "password": "string" }`  
  - **Resposta**: `200 OK` com `{ "token": "string" }`.  
  - **Autenticação**: Não requerida.
- `POST /api/auth/people`  
  - **Descrição**: Alias para criar uma pessoa (mesmo que `/api/people`).  
  - **Corpo da Requisição**: `{ "name": "string", "document": "string", "password": "string" }`  
  - **Resposta**: `201 Created` com os dados da pessoa.  
  - **Autenticação**: Não requerida.

**Nota**: Todas as rotas autenticadas requerem um cabeçalho `Authorization: Bearer <token>`.

## Pré-requisitos

- **Docker**: Para rodar a aplicação e o banco de dados em containers.
- **Git**: Para clonar o repositório.
- **Editor de Código**: Recomenda-se Visual Studio Code ou Visual Studio.

## Como Rodar o Projeto com Docker

Siga os passos abaixo para executar o projeto localmente usando Docker:

1. **Clone o Repositório**
   ```bash
   git clone https://github.com/seu-usuario/FinancialApi.git
   cd FinancialApi
   ## Como Rodar o Projeto com Docker

2. **Configure as Variáveis de Ambiente**
   - Crie ou edite o arquivo `.env` na raiz do projeto com as seguintes variáveis:
     ```plaintext
     # Banco de Dados
     ConnectionStrings__DefaultConnection=Host=host.docker.internal;Port=5432;Database=financialapidb;Username=postgres;Password=1234
     POSTGRES_DB=FinancialDb
     POSTGRES_USER=postgres
     POSTGRES_PASSWORD=1234

     # JWT
     JwtSettings__JwtKey=YourSuperSecretKeyWithMoreThan32Characters
     JwtSettings__JwtIssuer=FinancialApi
     JwtSettings__JwtAudience=FinancialApiUsers

     # API de Compliance
     ComplianceAuth__Email=seu-email@exemplo.com
     ComplianceAuth__Password=sua-senha-segura
     COMPLIANCE_API_BASE_URL=http://compliance-exemple

     # Ambiente da aplicação
     ASPNETCORE_ENVIRONMENT=Development