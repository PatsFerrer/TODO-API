using Dapper;
using Microsoft.Data.SqlClient;
using TodoListApi.DTOs.Todo;
using TodoListApi.Models;
using TodoListApi.Repositories.Interface;

namespace TodoListApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly string _connectionString;

        public TodoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateAsync(Todo todo)
        {
            var query = File.ReadAllText("Data/Todos/CreateTodo.sql");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, todo);
            }
        }

        public async Task<IEnumerable<TodoResponseDTO>> GetTodosByUserIdAsync(Guid userId)
        {
            var query = File.ReadAllText("Data/Todos/GetTodosByUserId.sql");

            using (var connection = new SqlConnection(_connectionString))
            {
                var todos = await connection.QueryAsync<TodoResponseDTO>(query, new { UserId = userId });
                return todos;
            }
        }

        public async Task<Todo?> GetByIdAsync(Guid id)
        {
            var query = File.ReadAllText("Data/Todos/GetTodoById.sql");

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Todo>(query, new { Id = id });
            }
        }

        public async Task UpdateAsync(Todo todo)
        {
            var query = File.ReadAllText("Data/Todos/UpdateTodo.sql");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new { todo.Id, todo.IsCompleted });
            }
        }
    }
}
