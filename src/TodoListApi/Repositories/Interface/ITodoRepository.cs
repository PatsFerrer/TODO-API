using TodoListApi.Models;

namespace TodoListApi.Repositories.Interface
{
    public interface ITodoRepository
    {
        Task CreateAsync(Todo todo);
    }
}
