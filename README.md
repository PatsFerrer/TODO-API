# 📝 To-Do List API

Esta é uma API REST feita com .NET 8 que simula uma lista de tarefas com autenticação de usuários. O objetivo é praticar a criação de um projeto realista com separação por camadas, uso de DTOs, autenticação com JWT e persistência de dados usando SQL Server + Dapper.

---

## 🚀 Tecnologias utilizadas

- .NET 8
- ASP.NET Core Web API
- SQL Server (via Docker)
- Dapper
- JWT (Json Web Token)
- Injeção de Dependência
- Variáveis de ambiente com `.env`
- Camadas separadas (Controller, Service, Repository, DTO, Model)

---

## 📦 Dependências NuGet

- `Dapper`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Microsoft.Data.SqlClient`

---

## ⚙️ Como rodar o projeto

### Clone o repositório:

```bash
git clone https://github.com/PatsFerrer/TODO-API.git
```

### Crie o banco de dados

O script de criação do banco já está pronto no arquivo `Data/InitDb.sql`.

Você só precisa rodar esse script em uma instância do SQL Server (pode ser local ou em um container Docker).

💡 Dica: para rodar via SSMS ou Azure Data Studio, basta abrir o arquivo `InitDb.sql` e executar.

### Configure a conexão com o banco

Já existe um arquivo `.env.example` com o modelo das variáveis de ambiente.

Crie seu `.env` com os dados reais de conexão ao banco SQL Server.

O `appsettings.json` já está preparado para usar essas variáveis.

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

Atenção: o arquivo `appsettings.Development.json` não está no repositório para evitar vazamento de senha. Crie esse arquivo localmente.

### Rode a aplicação
```bash
dotnet run
```

A API será iniciada e você poderá fazer chamadas para os endpoints usando ferramentas como Postman ou Insomnia.

## Funcionalidades já implementadas:
- [x] Criação de usuários com hash de senha seguro
- [x] Separação por camadas (Model, DTO, Repository, Service, Controller)
- [x] Persistência com Dapper
- [x] Configuração por variáveis de ambiente
- [x] Autenticação com JWT
- [ ] Validação de dados com FluentValidation

## Estrutura de pastas
```
TodoListApi/
│
├── Controllers/          # Controllers da API
├── DTOs/                 # Objetos de transferência de dados
├── Models/               # Entidades
├── Repositories/         # Acesso ao banco de dados (Dapper)
├── Services/             # Regras de negócio
├── Utils/ ou Security/   # Lógica auxiliar como hashing de senha
├── Data/                 # Scripts de criação de banco
└── Program.cs            # Configuração da aplicação
```

## 🔑 Endpoints
### Usuários
- `POST /api/user` - Cria um novo usuário
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
- `GET /api/todo` - Retorna a tarefa do usuário logado
  - Autenticação Necessária: Para acessar suas tarefas, inclua o seu Bearer Token no cabeçalho de autorização da requisição.
### Login
- `POST /api/login` - Autentica o usuário e retorna um token JWT (Bearer Token)
  - Corpo da Requisição (Body):
    ```json
    {
      "username": "string",
      "password": "string"
    }
    ```

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
