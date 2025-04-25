using System.Security.Claims;

namespace TodoListApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            return Guid.TryParse(userId, out var guid)
                ? guid
                : throw new UnauthorizedAccessException("Usuário não autenticado");
        }
    }
}
