using Dapper;
using Microsoft.Data.SqlClient;
using TodoListApi.Models;
using TodoListApi.Repositories.Interface;

namespace TodoListApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<User> CreateAsync(User user)
        {
            var query = File.ReadAllText("Data/Users/CreateUser.sql");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, user);
            }

            return user;

        }
    }
}
