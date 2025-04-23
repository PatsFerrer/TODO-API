using Dapper;
using Microsoft.Data.SqlClient;
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
            var query = File.ReadAllText("Data/CreateTodo.sql");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, todo);
            }
        }
    }
}
