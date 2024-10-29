using Shared;
using Shared.DTOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDTO userToCreate);
    public Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchParameters);
}