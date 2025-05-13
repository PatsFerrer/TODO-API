using System.Security.Claims;
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

        public async Task<IEnumerable<TodoResponseDTO>> GetTodosByUserAsync(ClaimsPrincipal user)
        {
            var userId = user.GetUserId();
            return await _repository.GetTodosByUserIdAsync(userId);
        }

        public async Task<bool> UpdateStatusAsync(Guid todoId, Guid userId, bool isCompleted)
        {
            var todo = await _repository.GetByIdAsync(todoId);

            if (todo == null || todo.UserId != userId) return false;

            todo.IsCompleted = isCompleted;
            await _repository.UpdateAsync(todo);
            return true;
        }

        public async Task<bool> DeleteTodoAsync(Guid todoId, Guid userId)
        {
            var todo = await _repository.GetByIdAsync(todoId);

            if (todo == null || todo.UserId != userId) return false;

            await _repository.DeleteAsync(todo);
            return true;
        }
    }
}
