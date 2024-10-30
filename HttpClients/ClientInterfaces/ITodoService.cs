using Shared;
using Shared.DTOs;

namespace HttpClients.ClientInterfaces;

public interface ITodoService
{
    Task CreateAsync(TodoCreationDTO dto);
}