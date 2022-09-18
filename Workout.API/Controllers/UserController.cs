using Microsoft.AspNetCore.Mvc;
using Workout.API.Authorization;
using Workout.Application.Models;
using Workout.Application.Services;
using Workout.Core.Querying;

namespace Workout.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _service;

    public UsersController(ILogger<UsersController> logger,
                           IUserService service)
    {
        _logger = logger;
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] UserModel user)
    {
        var userExists = await _service.AuthenticateUser(user.Username, user.Password);
        if (userExists)
        {
            return Ok();
        }

        return Unauthorized();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int pageIndex,
                                         [FromQuery] int pageSize)
    {
        var users = await _service.GetUserList(new PagingArgs
        {
            Index = pageIndex,
            Size = pageSize
        });

        return Ok(users);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _service.GetUserById(id);
        if (user != null)
        {
            return Ok(user);
        }

        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserModel user)
    {
        user = await _service.CreateUser(user);
        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> Put(int id, UserModel user)
    {
        var userControl = _service.GetUserById(id);
        if (userControl != null)
        {
            user = await _service.UpdateUser(id, user);
            return Ok(user);
        }

        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var userControl = _service.GetUserById(id);
        if (userControl != null)
        {
            await _service.DeleteUserById(id);
            return Ok();
        }

        return BadRequest();
    }
}
