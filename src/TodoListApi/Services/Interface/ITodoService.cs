using System.Security.Claims;
using TodoListApi.DTOs.Todo;

namespace TodoListApi.Services.Interface
{
    public interface ITodoService
    {
        Task<TodoResponseDTO> CreateTodoAsync(CreateTodoDTO dto, ClaimsPrincipal user);
        Task<IEnumerable<TodoResponseDTO>> GetTodosByUserAsync(ClaimsPrincipal user);

    }
}
