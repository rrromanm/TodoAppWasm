using Shared;
using Shared.DTOs;

namespace Application.DaoInterfaces;

public interface ITodoDao
{
    Task<Todo> CreateAsync(Todo todo);
    Task<IEnumerable<Todo>> GetByParameterAsync(SearchTodoParameterDTO searchTodoParameter);
    Task UpdateAsync(Todo updateTodo);
    Task<Todo> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    
}   