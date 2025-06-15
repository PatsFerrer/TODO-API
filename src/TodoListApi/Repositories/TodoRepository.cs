using Microsoft.EntityFrameworkCore;
using TodoListApi.DTOs.Todo;
using TodoListApi.Infra;
using TodoListApi.Models;
using TodoListApi.Repositories.Interface;

namespace TodoListApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoResponseDTO>> GetTodosByUserIdAsync(Guid userId)
        {
            return await _context.Todos
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .Select(t => new TodoResponseDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsCompleted = t.IsCompleted,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(Guid id)
        {
            return await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Todo todo)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }
}