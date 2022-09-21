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
    private readonly IWorkoutService _workoutService;
    private readonly IMovementService _movementService;

    public WorkoutsController(ILogger<WorkoutsController> logger,
                              IWorkoutService workoutService,
                              IMovementService movementService)
    {
        _logger = logger;
        _workoutService = workoutService;
        _movementService = movementService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] uint pageIndex,
                                         [FromQuery] uint pageSize)
    {
        var workouts = await _workoutService.GetWorkoutList(new PagingArgs(pageIndex, pageSize));
        return Ok(workouts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] uint id)
    {
        var workout = await _workoutService.GetWorkoutById(id);
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

        var createdUser = await _workoutService.CreateWorkout(workout);
        if (createdUser != null)
        {
            return Ok(createdUser);
        }

        _logger.LogError("Couldn't create workout. Workout: {0}:{1}", workout.Name, workout.Description);
        return BadRequest();
    }

    [HttpPut("{id}/Add/{movementId}")]
    public async Task<IActionResult> Add([FromRoute] uint id, [FromRoute] uint movementId)
    {
        var workout = await _workoutService.GetWorkoutById(id);
        var movement = await _movementService.GetMovementById(id);
        if (workout != null && movement != null)
        {
            var w = await _workoutService.AddMovementToWorkout(id, movementId);
            return Ok(w);
        }

        _logger.LogWarning("Workout or movement not found with given ids: {0}-", id, movementId);
        return BadRequest();
    }

    [HttpPut("{id}/Remove/{movementId}")]
    public async Task<IActionResult> Remove([FromRoute] uint id, [FromRoute] uint movementId)
    {
        var workout = await _workoutService.GetWorkoutById(id);
        var movement = await _movementService.GetMovementById(id);
        if (workout != null && movement != null)
        {
            var w = await _workoutService.RemoveMovementFromWorkout(id, movementId);
            return Ok(w);
        }

        _logger.LogWarning("Workout or movement not found with given ids: {0}-", id, movementId);
        return BadRequest();
    }


    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] uint id)
    {
        return Ok(await _workoutService.DeleteWorkoutById(id));
    }
}
