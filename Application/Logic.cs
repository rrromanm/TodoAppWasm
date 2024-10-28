using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared;
using Shared.DTOs;

namespace Application;

public class Logic : IUserLogic
{
    private readonly IUserDao _userDao;

    public Logic(IUserDao userDao)
    {
        _userDao = userDao;
    }
    
    public async Task<User> CreateAsync(UserCreationDTO userToCreate)
    {
        User? existingUser = await _userDao.GetByUserNameAsync(userToCreate.username);
        if (existingUser != null)
        {
            throw new ArgumentException("Username already exists.");
        }

        ValidateData(userToCreate);
        User toCreate = new User()
        {
            Username = userToCreate.username
        };

        User created = await _userDao.CreateAsync(toCreate);
        
        return created;
    }

    private static void ValidateData(UserCreationDTO dto)
    {
        string userName = dto.username;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }
}