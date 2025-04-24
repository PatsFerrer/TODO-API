using TodoListApi.DTOs.User;
using TodoListApi.Repositories.Interface;
using TodoListApi.Security;
using TodoListApi.Services.Interface;

namespace TodoListApi.Services.User
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
            var (hash, salt) = PasswordHasher.HashPassword(dto.Password);

            var user = new Models.User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                PasswordHash = hash,
                Salt = salt
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
