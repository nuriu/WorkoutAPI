namespace Workout.Core.Entities;

/// <summary>
/// Base mechanism for all entities.
/// </summary>
/// <typeparam name="TId">Type of the Primary Key.</typeparam>
public interface IBaseEntity<TId>
{
    /// <summary>
    /// Primary key of the entity.
    /// </summary>
    TId Id { get; }
    /// <summary>
    /// Creation date of the entity.
    /// </summary>
    DateTime CreatedAt { get; }
    /// <summary>
    /// Last update date of the entity.
    /// </summary>
    DateTime UpdatedAt { get; }
}
