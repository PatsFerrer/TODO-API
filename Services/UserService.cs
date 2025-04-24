using TodoListApi.DTOs.User;
using TodoListApi.Models;
using TodoListApi.Repositories.Interface;
using TodoListApi.Services.Interface;

namespace TodoListApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                PasswordHash = dto.Password
            };

            var createdUser = await _repository.CreateAsync(user);

            return new UserDTO
            {
                Id = createdUser.Id,
                Username = createdUser.Username
            };
        }
    }
}
