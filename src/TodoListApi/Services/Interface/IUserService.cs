using TodoListApi.DTOs.User;

namespace TodoListApi.Services.Interface
{
    public interface IUserService
    {
        Task<UserDTO> CreateUserAsync(CreateUserDTO dto);
        //Task<UserDTO> GetUserByIdAsync(Guid id);
        //Task<IEnumerable<UserDTO>> GetAllUsers();
    }
}
