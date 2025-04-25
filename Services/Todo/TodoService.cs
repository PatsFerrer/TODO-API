using System.Security.Claims;
using System.Threading.Tasks;
using TodoListApi.DTOs.Todo;
using TodoListApi.Extensions;
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

        public async Task<TodoResponseDTO> CreateTodoAsync(CreateTodoDTO dto, ClaimsPrincipal user)
        {
            var todo = new Models.Todo
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = user.GetUserId()
            };

            await _repository.CreateAsync(todo);

            return new TodoResponseDTO
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt
            };
        }
    }
}
