using TodoListApi.DTOs.Todo;
using TodoListApi.Models;

namespace TodoListApi.Repositories.Interface
{
    public interface ITodoRepository
    {
        Task CreateAsync(Todo todo);
        Task<IEnumerable<TodoResponseDTO>> GetTodosByUserIdAsync(Guid userId);
    }
}
