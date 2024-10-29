using Shared;
using Shared.DTOs;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUserNameAsync(string userName);
    public Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchUserParameter);
    public Task<User?> GetByIdAsync(int id);
}