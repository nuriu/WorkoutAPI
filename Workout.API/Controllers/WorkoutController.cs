using Microsoft.AspNetCore.Mvc;
using Workout.API.Authorization;
using Workout.Application.Models;
using Workout.Application.Services;
using Workout.Core.Querying;

namespace Workout.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WorkoutsController : ControllerBase
{
    private readonly ILogger<WorkoutsController> _logger;
    private readonly IWorkoutService _service;

    public WorkoutsController(ILogger<WorkoutsController> logger,
                              IWorkoutService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] uint pageIndex,
                                         [FromQuery] uint pageSize)
    {
        var workouts = await _service.GetWorkoutList(new PagingArgs(pageIndex, pageSize));
        return Ok(workouts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] uint id)
    {
        var workout = await _service.GetWorkoutById(id);
        if (workout != null)
        {
            return Ok(workout);
        }

        _logger.LogWarning("Workout not found with given id: {0}", id);
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] WorkoutModel workout)
    {
        workout.CreatorId = (uint)HttpContext.Items["UserId"];
        workout.UpdaterId = workout.CreatorId;

        var createdUser = await _service.CreateWorkout(workout);
        if (createdUser != null)
        {
            return Ok(createdUser);
        }

        _logger.LogError("Couldn't create workout. Workout: {0}:{1}", workout.Name, workout.Description);
        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] uint id)
    {
        return Ok(await _service.DeleteWorkoutById(id));
    }
}
