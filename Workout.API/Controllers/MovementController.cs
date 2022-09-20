using Microsoft.AspNetCore.Mvc;
using Workout.API.Authorization;
using Workout.Application.Models;
using Workout.Application.Services;
using Workout.Core.Querying;

namespace Workout.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class MovementsController : ControllerBase
{
    private readonly ILogger<MovementsController> _logger;
    private readonly IMovementService _service;

    public MovementsController(ILogger<MovementsController> logger,
                               IMovementService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] uint pageIndex,
                                         [FromQuery] uint pageSize)
    {
        var movements = await _service.GetMovementList(new PagingArgs(pageIndex, pageSize));
        return Ok(movements);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] uint id)
    {
        var movement = await _service.GetMovementById(id);
        if (movement != null)
        {
            return Ok(movement);
        }

        _logger.LogWarning("Movement not found with given id: {0}", id);
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MovementModel movement)
    {
        movement.CreatorId = (uint)HttpContext.Items["UserId"];
        movement.UpdaterId = movement.CreatorId;

        var createdUser = await _service.CreateMovement(movement);
        if (createdUser != null)
        {
            return Ok(createdUser);
        }

        _logger.LogError("Couldn't create movement. Movement: {0}:{1}", movement.Name, movement.Description);
        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] uint id)
    {
        return Ok(await _service.DeleteMovementById(id));
    }
}
