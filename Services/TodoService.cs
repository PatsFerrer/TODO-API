using TodoListApi.DTOs.Todo;
using TodoListApi.Models;
using TodoListApi.Repositories.Interface;

namespace TodoListApi.Services
{
    public class TodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateTodoAsync(CreateTodoDTO dto)
        {
            var todo = new Todo
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = dto.UserId
            };

            await _repository.CreateAsync(todo);
        }
    }
}
