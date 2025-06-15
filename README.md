# 📝 To-Do List API com .NET 8 e Docker

Esta é uma API RESTful completa construída com .NET 8, simulando uma lista de tarefas (To-Do list) com autenticação de usuários e persistência de dados. O projeto foi desenvolvido com foco em boas práticas, incluindo uma arquitetura em camadas, uso de DTOs, autenticação segura com JWT e um ambiente de desenvolvimento totalmente containerizado com Docker.

O banco de dados é gerenciado automaticamente através do Entity Framework Core Migrations, o que significa que ao iniciar a aplicação, a estrutura do banco (tabelas, relacionamentos, etc.) é criada ou atualizada sozinha.

---

## 🚀 Tecnologias utilizadas

- .NET 8
- Backend: .NET 8, ASP.NET Core Web API
- Banco de Dados: SQL Server (rodando em Docker)
- ORM: Entity Framework Core 8
- Containerização: Docker & Docker Compose
- Autenticação: JWT (JSON Web Tokens)
- Arquitetura: Injeção de Dependência, Padrão de Repositório, Camadas de Serviço.

---

## 📦 Dependências NuGet

- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.AspNetCore.Authentication.JwtBearer`

---

## ⚙️ Como Rodar o Projeto Localmente (com Docker)
O projeto é configurado para rodar facilmente em qualquer ambiente com Docker.

### Pré-requisitos
- [Docker](https://www.docker.com/products/docker-desktop/) instalado e rodando.

1. Clone o Repositório
```bash
git clone https://github.com/PatsFerrer/TODO-API.git
```

2. Configure os Segredos
  A configuração de segredos é feita em dois locais diferentes:

    a) Para o Docker (.env)
    Este arquivo passa a senha do banco de dados para o contêiner do SQL Server.
  
    1. Na raiz do projeto, crie um arquivo chamado `.env`.
    2. Adicione a senha do banco:
  
    Exemplo do arquivo `.env`:
    ```bash
    # Senha para o usuário 'sa' do SQL Server (precisa ser forte!)
    SQL_SA_PASSWORD=YourAmazingSecretKey!123
    ```

    b) Para a API (appsettings.Development.json)
    Este arquivo guarda a chave secreta do JWT para o ambiente de desenvolvimento local. Este arquivo não deve ir para o GitHub.
  
    1. Crie um arquivo chamado `appsettings.Development.json`.
    2. Cole o seguinte conteúdo e adicione sua chave:
  
    Exemplo de string de conexão:
    ```bash
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost,1433;Database=YourDatabase;User Id=yourId;Password=yourPassword;TrustServerCertificate=True"
      },
      "Jwt": {
        "Key": "your-amazing-secret-key-here-xP"
      },
      "AllowedHosts": "*"
    }
    ```

3. Suba os Contêineres
   Com o Docker em execução, rode o seguinte comando na raiz do projeto:
  ```bash
  docker-compose up --build
  ```
Este comando irá:

Construir a imagem Docker da API .NET.
Iniciar um contêiner com o SQL Server.
Iniciar o contêiner da API.

A API será iniciada e você poderá fazer chamadas para os endpoints usando ferramentas como Postman ou Insomnia.

## Funcionalidades e Boas Práticas

- [x] API RESTful completa com operações CRUD.
- [x] Autenticação Segura com JSON Web Tokens (JWT).
- [x] Criação de usuários com hash de senha e salt para maior segurança.
- [x] Arquitetura em Camadas bem definida (Controllers, Services, Repositories).
- [x] Persistência de Dados robusta com Entity Framework Core 8.
- [x] Gerenciamento de Banco de Dados automático com EF Core Migrations.
- [x] Ambiente de Desenvolvimento completo e isolado com Docker e Docker Compose.
- [x] Configuração flexível com appsettings.json e variáveis de ambiente (.env).
- [x] Pronta para Deploy em plataformas de nuvem como o Coolify.
- [ ] Validação de dados com FluentValidation (próximo passo).

## Estrutura de pastas
```
.
├── src/TodoListApi/
│   ├── Controllers/       # Controllers da API (endpoints)
│   ├── DTOs/              # Objetos de Transferência de Dados
│   ├── Infra/             # Configuração do DbContext do EF Core
│   ├── Migrations/        # Scripts de migração gerados pelo EF Core
│   ├── Models/            # Entidades do banco de dados
│   ├── Repositories/      # Acesso ao banco de dados (Entity Framework Core)
│   ├── Services/          # Regras de negócio
│   ├── Auth/              # Lógica de autenticação e geração de token
│   └── Program.cs         # Configuração da aplicação e inicialização
│
├── .gitignore
├── docker-compose.yml     # Orquestração dos contêineres
├── Dockerfile             # Definição da imagem da API
└── README.md
```

## 🔑 Endpoints
A API está disponível em `http://localhost:8080`.
### Usuários
- `POST /api/user` - Cria um novo usuário
  - Corpo da Requisição (Body):
    ```json
    {
      "username": "string",
      "password": "string"
    }
    ```
    
### Login
- `POST /api/login` - Autentica o usuário e retorna um token JWT (Bearer Token)
  - Corpo da Requisição (Body):
    ```json
    {
      "username": "string",
      "password": "string"
    }
    ```
    
### Tarefas
- `POST /api/todo` - Cria uma nova tarefa (O usuário deve estar logado)
  - Autenticação Necessária: Para criar uma tarefa, você precisa estar autenticado. Envie o seu Bearer Token no cabeçalho de autorização da requisição.
  - Corpo da Requisição (Body):
    ```json
    {
      "title": "string",
    }
    ```
- `PATCH /api/todo/status/{id}` - Atualiza o status de uma tarefa específica.
  - Corpo (Body):
    ```json
    {
      "isCompleted": boolean
    }
    ```

- `GET /api/todo` - Retorna a tarefa do usuário logado
  - Autenticação Necessária: Para acessar suas tarefas, inclua o seu Bearer Token no cabeçalho de autorização da requisição.

- `DELETE /api/todo/{id}` - Deleta uma tarefa.

Observação Importante sobre Autenticação:

Para acessar os endpoints protegidos (como a criação e listagem de tarefas), você precisará obter um Bearer Token através do endpoint de `/api/login`. Após o login bem-sucedido, o token retornado deve ser incluído no cabeçalho `Authorization` das suas requisições para os endpoints protegidos. O formato do cabeçalho deve ser:
`Authorization: Bearer <seu_token_aqui>`

Exemplo de como incluir o Bearer Token em uma requisição (usando curl):
- PowerShell
```PowerShell
curl 'https://localhost:7264/api/todo' -Method GET -Headers @{'Authorization'='Bearer SEU_TOKEN_JWT'}
```

- CMD
```cmd
curl -X GET "https://localhost:7264/api/todo" -H "Authorization: Bearer SEU_TOKEN_JWT"
```

## 🧪 Testes
Por enquanto os testes estão sendo feitos manualmente via Postman. Em breve será adicionado um projeto de testes automatizados.
