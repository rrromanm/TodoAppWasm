using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.DTOs;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class TodoController : ControllerBase
{
    private readonly ITodoLogic _todoLogic;
    
    public TodoController(ITodoLogic todoLogic, IUserLogic userLogic)
    {
        _todoLogic = todoLogic;
    }
    
    // POST localhost:7124/todos
    [HttpPost]
    public async Task<IActionResult> Create(TodoCreationDTO todoToCreate)
    {
        try
        {
            Todo todo = await _todoLogic.createAsync(todoToCreate);
            return Created($"/todos/{todo.Id}", todo);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, ex.Message);
        }
    }
    
    // GET localhost:7124/todos/{Id}
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId,
        [FromQuery] bool? completedStatus, [FromQuery] string? titleContains)
    {
        try
        {   
            SearchTodoParameterDTO search = new (userName, userId, completedStatus, titleContains);
            var todos = await _todoLogic.getTodosByParameterAsync(search);
            return Ok(todos);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, ex.Message);
        }
    }
    
    // PATCH localhost:7124/todosUpdate
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] TodoUpdateDTO dto)
    {
        try
        {
            await _todoLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    // DELETE localhost:7124/todos/{Id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await _todoLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    // GET localhost:7124/todos/{Id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoBasicDTO>> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            TodoBasicDTO todo = await _todoLogic.GetByIdAsync(id);
            return Ok(todo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}