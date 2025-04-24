using TodoListApi.Models;

namespace TodoListApi.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetByUsernameAsync(string username);
        //Task<IEnumerable<User>> GetAllAsync();
    }
}
