using TodoListApi.Models;

namespace TodoListApi.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        //Task<IEnumerable<User>> GetAllAsync();
    }
}
