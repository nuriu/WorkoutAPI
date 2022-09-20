using Microsoft.AspNetCore.Mvc;
using Workout.API.Authorization;
using Workout.Application.Models;
using Workout.Application.Services;
using Workout.Core.Querying;

namespace Workout.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class MuscleGroupsController : ControllerBase
{
    private readonly ILogger<MuscleGroupsController> _logger;
    private readonly IMuscleGroupService _service;

    public MuscleGroupsController(ILogger<MuscleGroupsController> logger,
                                  IMuscleGroupService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] uint pageIndex,
                                         [FromQuery] uint pageSize)
    {
        var muscleGroups = await _service.GetMuscleGroupList(new PagingArgs(pageIndex, pageSize));
        return Ok(muscleGroups);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] uint id)
    {
        var muscleGroup = await _service.GetMuscleGroupById(id);
        if (muscleGroup != null)
        {
            return Ok(muscleGroup);
        }

        _logger.LogWarning("Muscle group not found with given id: {0}", id);
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MuscleGroupModel muscleGroup)
    {
        muscleGroup.CreatorId = (uint)HttpContext.Items["UserId"];
        muscleGroup.UpdaterId = muscleGroup.CreatorId;

        var createdUser = await _service.CreateMuscleGroup(muscleGroup);
        if (createdUser != null)
        {
            return Ok(createdUser);
        }

        _logger.LogError("Couldn't create muscle group. Muscle Group: {0}:{1}", muscleGroup.Name, muscleGroup.Description);
        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] uint id)
    {
        return Ok(await _service.DeleteMuscleGroupById(id));
    }
}
