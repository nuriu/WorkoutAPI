using Workout.Core.Entities;

namespace Workout.Core.Repositories;

public interface IMovementRepository : IRepository<Movement>
{
    /// <summary>
    /// Retrieves movements for the workout.
    /// </summary>
    /// <param name="workoutId">Workout id to match movements from.</param>
    /// <returns>List of movements.</returns>
    Task<IReadOnlyList<Movement>?> GetMovementsByWorkoutIdAsync(uint workoutId);
}
