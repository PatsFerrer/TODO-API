using Microsoft.Data.SqlClient;
using TodoListApi.DTOs.User;
using TodoListApi.Exceptions;
using TodoListApi.Repositories.Interface;
using TodoListApi.Security;
using TodoListApi.Services.Interface;

namespace TodoListApi.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository repository, ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO dto)
        {
            var (hash, salt) = PasswordHasher.HashPassword(dto.Password);

            var user = new Models.User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                PasswordHash = hash,
                Salt = salt
            };

            try
            {
                var createdUser = await _repository.CreateAsync(user);

                return new UserDTO
                {
                    Id = createdUser.Id,
                    Username = createdUser.Username
                };
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                _logger.LogWarning(ex, "[CREATE USER] User already exists: {Username}", dto.Username);
                throw new UsernameAlreadyExistsException();
            }
        }
    }
}
