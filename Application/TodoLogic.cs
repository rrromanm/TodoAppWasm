using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared;
using Shared.DTOs;

namespace Application;

public class TodoLogic : ITodoLogic
{
    private readonly ITodoDao _todoDao;
    private readonly IUserDao _userDao;
    
    public TodoLogic(ITodoDao todoDao, IUserDao userDao)
    {
        _todoDao = todoDao;
        _userDao = userDao;
    }


    public async Task<Todo> createAsync(TodoCreationDTO todoToCreate)
    {
        User? user = await _userDao.GetByIdAsync(todoToCreate.UserId);
        if (user == null)
        {
            throw new InvalidOperationException($"User with id {todoToCreate.UserId} not found");
        }

        ValidateTodo(todoToCreate);
        Todo todo = new Todo(user, todoToCreate.Title);
        Todo created = await _todoDao.CreateAsync(todo);
        return created;
    }

    public Task<IEnumerable<Todo>> getTodosByParameterAsync(SearchTodoParameterDTO searchTodoParameter)
    {
        return _todoDao.GetByParameterAsync(searchTodoParameter);
    }

    public async Task<TodoBasicDTO> GetByIdAsync(int id)
    {
        Todo? existingTodo = await _todoDao.GetByIdAsync(id);
        if (existingTodo == null)
        {
            throw new Exception($"Todo with ID {id} was not found!");
        }

        return new TodoBasicDTO(existingTodo.Id, existingTodo.Owner.Username, existingTodo.Title, existingTodo.IsCompleted);
    }

    public async Task UpdateAsync(TodoUpdateDTO dto)
    {
        Todo? existing = await _todoDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Todo with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await _userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        if (dto.IsCompleted != null && existing.IsCompleted && !(bool)dto.IsCompleted)
        {
            throw new Exception("Cannot un-complete a completed Todo");
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        bool completedToUse = dto.IsCompleted ?? existing.IsCompleted;
        
        Todo updated = new (userToUse, titleToUse)
        {
            IsCompleted = completedToUse,
            Id = existing.Id,
        };

        ValidateTodo(updated);

        await _todoDao.UpdateAsync(updated);
        
    }

    public async Task DeleteAsync(int id)
    {
        Todo? todo = await _todoDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Todo with ID {id} was not found!");
        }

        if (!todo.IsCompleted)
        {
            throw new Exception("Cannot delete un-completed Todo!");
        }

        await _todoDao.DeleteAsync(id);
    }

    private void ValidateTodo(TodoCreationDTO todoToCreate)
    {
        if (string.IsNullOrWhiteSpace(todoToCreate.Title))
        {
            throw new InvalidOperationException("Title is required");
        }
    }
    
    private void ValidateTodo(Todo dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}