using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;
using TodoListApi.Constants;
using TodoListApi.Exceptions;

namespace TodoListApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var errorCode = ErrorCode.InternalServerError;
            var message = "Algo deu errado. Por favor, tente novamente mais tarde.";

            switch (exception)
            {
                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errorCode = ErrorCode.Unauthorized;
                    message = "Acesso não autorizado.";
                    break;
                case InvalidCredentialsException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errorCode = ErrorCode.InvalidCredentials;
                    message = exception.Message;
                    break;
                case KeyNotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    errorCode = ErrorCode.NotFound;
                    message = "O recurso solicitado não foi encontrado.";
                    break;
                case UsernameAlreadyExistsException:
                    statusCode = (int)HttpStatusCode.Conflict;
                    errorCode = ErrorCode.UsernameAlreadyExists;
                    message = exception.Message;
                    break;
                case SqlException:
                    statusCode = (int)HttpStatusCode.ServiceUnavailable;
                    errorCode = ErrorCode.DatabaseError;
                    message = "Serviço temporariamente indisponível.";
                    break;
                case ArgumentException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorCode = ErrorCode.BadRequest;
                    message = exception.Message;
                    break;
            }

            _logger.LogError(exception, "Erro inesperado: {Message}", exception.Message);

            var response = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = message,
                ErrorCode = errorCode
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));

        }
    }
}
