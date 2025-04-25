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

1. Clone o repositório:

```bash
git clone https://github.com/PatsFerrer/TODO-API.git
```

2. Crie o banco de dados
O script de criação do banco já está pronto no arquivo `Data/InitDb.sql`.
Você só precisa rodar esse script em uma instância do SQL Server (pode ser local ou em um container Docker).

💡 Dica: para rodar via SSMS ou Azure Data Studio, basta abrir o arquivo InitDb.sql e executar.

3. Configure a conexão com o banco
Já existe um arquivo .env.example com o modelo das variáveis de ambiente.

Crie seu .env com os dados reais de conexão ao banco SQL Server.

O appsettings.json já está preparado para usar essas variáveis.

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

Atenção: o arquivo appsettings.Development.json não está no repositório para evitar vazamento de senha. Crie esse arquivo localmente.

4. Rode a aplicação
```bash
dotnet run
```

A API será iniciada e você poderá fazer chamadas para os endpoints usando ferramentas como Postman ou Insomnia.

Funcionalidades já implementadas:
`[x]` Criação de usuários com hash de senha seguro
`[x]` Separação por camadas (Model, DTO, Repository, Service, Controller)
`[x]` Persistência com Dapper
`[x]` Configuração por variáveis de ambiente
`[x]` Autenticação com JWT
`[]` Validação de dados com FluentValidation

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
  - Body:
    ```json
    {
      "username": "string",
      "password": "string"
    }
    ```
### Tarefas
- `POST /api/todos` - Cria uma nova tarefa
  - Body:
    ```json
    {
      "title": "string",
      "description": "string"
    }
    ```
### Login
- `POST /api/login` - Faz login e retorna um token JWT
  - Body:
    ```json
    {
      "username": "string",
      "password": "string"
    }
    ```

## 🧪 Testes
Por enquanto os testes estão sendo feitos manualmente via Postman. Em breve será adicionado um projeto de testes automatizados.
