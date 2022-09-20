using Workout.Application.Models;
using Workout.Core.Querying;

namespace Workout.Application.Services;

/// <summary>
/// Bridge between repository and api.
/// </summary>
public interface IWorkoutService
{
    Task<IPagedList<WorkoutModel>?> GetWorkoutList(PagingArgs pagingArgs);
    Task<WorkoutModel?> GetWorkoutById(uint workoutId);
    Task<WorkoutModel?> CreateWorkout(WorkoutModel workout);
    Task<bool> DeleteWorkoutById(uint workoutId);
}
