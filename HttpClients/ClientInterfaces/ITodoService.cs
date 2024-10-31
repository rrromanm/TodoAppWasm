using Shared;
using Shared.DTOs;

namespace HttpClients.ClientInterfaces;

public interface ITodoService
{
    Task CreateAsync(TodoCreationDTO dto);
    Task<ICollection<Todo> > GetTodosAsync(string? username, int? userId, bool? isCompleted, string? titleContains);
    Task UpdateAsync (TodoUpdateDTO dto);
    Task<TodoBasicDTO> GetByIdAsync(int id);
    
    
}