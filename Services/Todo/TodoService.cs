using TodoListApi.DTOs.Todo;
using TodoListApi.Repositories.Interface;
using TodoListApi.Services.Interface;

namespace TodoListApi.Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateTodoAsync(CreateTodoDTO dto)
        {
            var todo = new Models.Todo
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
