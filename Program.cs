using TodoListApi.Repositories;
using TodoListApi.Repositories.Interface;
using TodoListApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona Controllers
builder.Services.AddControllers();

// Adiciona Reposit�rios e Servi�os
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<TodoService>();

// Permite usar vari�veis de ambiente
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

// Pipeline HTTP.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
