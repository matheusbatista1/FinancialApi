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

## Pré-requisitos

- **Docker**: Para rodar a aplicação e o banco de dados em containers.
- **Git**: Para clonar o repositório.
- **Editor de Código**: Recomenda-se Visual Studio Code ou Visual Studio.

## Como Rodar o Projeto com Docker

Siga os passos abaixo para executar o projeto localmente usando Docker:

### 1. Clone o Repositório

```bash
git clone https://github.com/matheusbatista1/FinancialApi.git
cd FinancialApi
```

### 2. Configure as Variáveis de Ambiente

Crie ou edite o arquivo `.env` na raiz do projeto com as seguintes variáveis:

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
```

### 3. Inicie os Serviços

Execute o seguinte comando na raiz do projeto:

```bash
docker-compose up --build
```

Isso construirá a imagem da API e iniciará os containers `api` e `db`.

### 4. Verifique a Execução

- A API estará disponível em [http://localhost:7045](http://localhost:7045).
- Se o Swagger estiver habilitado (em ambiente Development), acesse [http://localhost:7045/swagger](http://localhost:7045/swagger) para explorar e testar as rotas.
- O banco de dados estará disponível em `localhost:5432`.

### 5. Pare os Serviços

Para parar os containers, use:

```bash
docker-compose down
```

### 6. Executando os Testes Unitários

Para rodar os testes unitários do projeto, utilize o comando abaixo na raiz da solução ou dentro da pasta do projeto de testes:

```bash
dotnet test
```

Isso irá compilar a solução e executar todos os testes unitários implementados, exibindo o resultado no terminal.

---

## Licença

Este projeto está sob a MIT License. Veja o arquivo LICENSE para mais detalhes.

## Contato

Para dúvidas ou sugestões, entre em contato com seu
