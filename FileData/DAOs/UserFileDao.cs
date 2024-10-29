using Application.DaoInterfaces;
using Shared;
using Shared.DTOs;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;
    
    public UserFileDao(FileContext context)
    {
        this.context = context;
    }
    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id) + 1;
        }
        
        user.Id = userId;
        context.Users.Add(user);
        context.SaveChanges();
        
        return Task.FromResult(user);
    }

    public Task<User?> GetByUserNameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchUserParameter)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchUserParameter.UserNameContains != null)
        {
            users = users.Where(u => u.Username.Contains(searchUserParameter.UserNameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? existingUser = context.Users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(existingUser);
    }
    public void UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}