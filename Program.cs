using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoListApi.Auth;
using TodoListApi.Auth.Interface;
using TodoListApi.Repositories;
using TodoListApi.Repositories.Interface;
using TodoListApi.Services.Interface;
using TodoListApi.Services.Todo;
using TodoListApi.Services.User;

var builder = WebApplication.CreateBuilder(args);

// Adiciona Controllers
builder.Services.AddControllers();

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

// Adiciona Repositórios e Serviços
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();


// Permite usar variáveis de ambiente
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

// Pipeline HTTP.
app.UseHttpsRedirection();

app.UseAuthentication(); // <-- isso ativa o middleware de autenticação
app.UseAuthorization();

app.MapControllers();

app.Run();
