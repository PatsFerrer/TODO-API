using TodoListApi.DTOs.Auth;

namespace TodoListApi.Auth.Interface
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginRequestDTO dto);
    }
}
