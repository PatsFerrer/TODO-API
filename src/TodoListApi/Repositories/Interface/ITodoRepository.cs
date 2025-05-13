using TodoListApi.DTOs.Todo;
using TodoListApi.Models;

namespace TodoListApi.Repositories.Interface
{
    public interface ITodoRepository
    {
        Task CreateAsync(Todo todo);
        Task<IEnumerable<TodoResponseDTO>> GetTodosByUserIdAsync(Guid userId);
        Task<Todo?> GetByIdAsync(Guid id);
        Task UpdateAsync(Todo todo);
        Task DeleteAsync(Todo todo);
    }
}
