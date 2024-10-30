using Shared;
using Shared.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDTO dto);
    Task<IEnumerable<User>> GetUsersAsync(string? usernameContains = null);
}