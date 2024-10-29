using Shared;
using Shared.DTOs;

namespace Application.LogicInterfaces;

public interface ITodoLogic
{
    Task<Todo> createAsync(TodoCreationDTO todoToCreate);
    Task<IEnumerable<Todo>> getTodosByParameterAsync(SearchTodoParameterDTO searchTodoParameter);
    Task<TodoBasicDTO> GetByIdAsync(int id);
    Task UpdateAsync(TodoUpdateDTO todo);
    Task DeleteAsync(int id);
}