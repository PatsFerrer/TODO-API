using TodoListApi.Repositories;
using TodoListApi.Repositories.Interface;
using TodoListApi.Services;
using TodoListApi.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Adiciona Controllers
builder.Services.AddControllers();

// Adiciona Repositórios e Serviços
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IUserService, UserService>();

// Permite usar variáveis de ambiente
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

// Pipeline HTTP.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
