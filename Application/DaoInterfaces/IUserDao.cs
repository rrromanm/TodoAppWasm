using Shared;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUserNameAsync(string userName);
    void UpdateAsync(User user);
    Task<User> DeleteAsync(int id);
}