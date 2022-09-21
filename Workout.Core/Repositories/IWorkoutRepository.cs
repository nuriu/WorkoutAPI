namespace Workout.Core.Repositories;

public interface IWorkoutRepository : IRepository<Core.Entities.Workout>
{
    /// <summary>
    /// Adds specific movement record to the specified workout.
    /// </summary>
    /// <param name="workoutId">Primary key value of the target workout.</param>
    /// <param name="movementId">Primary key value of the target mvement.</param>
    /// <returns>Workout entity that has the given id.</returns>
    Task<Core.Entities.Workout?> AddMovementToWorkoutAsync(uint workoutId, uint movementId);

    /// <summary>
    /// Removes specific movement record from the specified workout.
    /// </summary>
    /// <param name="workoutId">Primary key value of the target workout.</param>
    /// <param name="movementId">Primary key value of the target mvement.</param>
    /// <returns>Workout entity that has the given id.</returns>
    Task<Core.Entities.Workout?> RemoveMovementFromWorkoutAsync(uint workoutId, uint movementId);

    Task<IReadOnlyList<Core.Entities.Workout>?> SearchWorkoutsAsync(ushort duration, uint difficultyLevelId, uint muscleGroupId);
}
