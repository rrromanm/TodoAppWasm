using Application.DaoInterfaces;
using Shared;
using Shared.DTOs;

namespace FileData.DAOs;

public class TodoFileDao : ITodoDao
{
    private readonly FileContext _context;
    
    public TodoFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Todo> CreateAsync(Todo todo)
    {
        int todoId = 1;
        if (_context.Todos.Any())
        {
            todoId = _context.Todos.Max(u => u.Id) + 1;
        }
        
        todo.Id = todoId;
        _context.Todos.Add(todo);
        _context.SaveChanges();
        
        return Task.FromResult(todo);
    }

    public Task<IEnumerable<Todo>> GetByParameterAsync(SearchTodoParameterDTO searchParams)
    {
        IEnumerable<Todo> result = _context.Todos.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            result = _context.Todos.Where(todo =>
                todo.Owner.Username.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParams.UserId != null)
        {
            result = result.Where(t => t.Owner.Id == searchParams.UserId);
        }

        if (searchParams.CompletedStatus != null)
        {
            result = result.Where(t => t.IsCompleted == searchParams.CompletedStatus);
        }

        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            result = result.Where(t =>
                t.Title.Contains(searchParams.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public Task UpdateAsync(Todo updateTodo)
    {
        Todo? existingTodo = _context.Todos.FirstOrDefault(t => t.Id == updateTodo.Id);
        if (existingTodo == null)
        {
            throw new InvalidOperationException($"Todo with ID {updateTodo.Id} not found!");
        }
        
        _context.Todos.Remove(existingTodo);
        _context.Todos.Add(updateTodo);
        _context.SaveChanges();
        
        return Task.CompletedTask;
    }

    public Task<Todo> GetByIdAsync(int id)
    {
        try
        {
            Todo? existingTodo = _context.Todos.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(existingTodo);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Todo with ID {id} not found!");
        }
    }

    public Task DeleteAsync(int id)
    {
        Todo? existingTodo = _context.Todos.FirstOrDefault(t => t.Id == id);
        if (existingTodo == null)
        {
            throw new InvalidOperationException($"Todo with ID {id} not found!");
        }
        
        _context.Todos.Remove(existingTodo);
        _context.SaveChanges();
        
        return Task.CompletedTask;
    }
}