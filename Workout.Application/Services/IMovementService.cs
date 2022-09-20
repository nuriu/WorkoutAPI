using Workout.Application.Models;
using Workout.Core.Querying;

namespace Workout.Application.Services;

/// <summary>
/// Bridge between repository and api.
/// </summary>
public interface IMovementService
{
    Task<IPagedList<MovementModel>?> GetMovementList(PagingArgs pagingArgs);
    Task<MovementModel?> GetMovementById(uint movementId);
    Task<MovementModel?> CreateMovement(MovementModel movement);
    Task<bool> DeleteMovementById(uint movementId);
}
