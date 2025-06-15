using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using TodoListApi.Auth;
using TodoListApi.Auth.Interface;
using TodoListApi.Infra;
using TodoListApi.Middlewares;
using TodoListApi.Repositories;
using TodoListApi.Repositories.Interface;
using TodoListApi.Services.Interface;
using TodoListApi.Services.Todo;
using TodoListApi.Services.User;
using TodoListApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Adiciona Controllers
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        // Esta linha garante que as datas sejam enviadas em UTC com 'Z'
        options.JsonSerializerOptions.Converters.Add(new DateTimeUtcConverter());
    });

var jwtKey = builder.Configuration["Jwt:Key"]; // pega do appsettings ou .env
if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("JWT Key not found in the configuration.");
}
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

// Pega a string de conexao do appsettings.json ou .env
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registra o DbContext para injecao de dependencia
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(connectionString));

// Adiciona Repositórios e Serviços
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Permite usar variáveis de ambiente
builder.Configuration.AddEnvironmentVariables();

// Configuração do CORS pro Front:
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
}); // até aqui

var app = builder.Build();

app.UseCors("AllowFrontend");

// Pipeline HTTP.
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>(); // <-- isso ativa o middleware de tratamento de erros

app.UseAuthentication(); // <-- isso ativa o middleware de autenticação
app.UseAuthorization();

app.MapControllers();

// RODA A MIGRACAO AUTOMATICAMENTE
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    dbContext.Database.Migrate();
}

app.Run();