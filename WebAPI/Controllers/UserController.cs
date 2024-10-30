using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.DTOs;

namespace WebAPI.Controllers;

[ApiController]
[Route("users")]

public class UserController : ControllerBase
{
    private readonly IUserLogic userLogic;
    
    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserCreationDTO userToCreate)
    {
        try
        {
            User user = await userLogic.CreateAsync(userToCreate);
            return Created($"/users/{user.Id}", user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetByParameter([FromQuery] string? username)
    {
        try
        {
            SearchUserParameterDTO searchParameters = new(username);
            IEnumerable<User>users = await userLogic.GetAsync(searchParameters);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}