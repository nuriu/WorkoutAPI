using Microsoft.AspNetCore.Mvc;
using Workout.API.Authorization;
using Workout.Application.Models;
using Workout.Application.Services;
using Workout.Core.Querying;

namespace Workout.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
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
    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserLoginModel user)
    {
        var userExists = await _service.AuthenticateUser(user.Username, user.Password);
        if (userExists)
        {
            return Ok();
        }

        _logger.LogWarning("Unauthorized user: {0}", user.Username);
        return Unauthorized();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int pageIndex,
                                         [FromQuery] int pageSize)
    {
        var users = await _service.GetUserList(new PagingArgs(pageIndex, pageSize));
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var user = await _service.GetUserById(id);
        if (user != null)
        {
            return Ok(user);
        }

        _logger.LogWarning("User not found with given id: {0}", id);
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserLoginModel user)
    {
        var createdUser = await _service.CreateUser(user);
        if (createdUser != null)
        {
            return Ok(createdUser);
        }

        _logger.LogError("Couldn't create user. User: {0}:{1}", user.Username, user.Password);
        return BadRequest();
    }
}
