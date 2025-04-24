using TodoListApi.DTOs.Todo;

namespace TodoListApi.Services.Interface
{
    public interface ITodoService
    {
        Task CreateTodoAsync(CreateTodoDTO dto);
    }
}
