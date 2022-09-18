namespace Workout.Core.Entities;

public abstract class BaseEntity<TId> : IBaseEntity<TId>
{
    public virtual TId Id { get; protected set; }

    public virtual DateTime CreatedAt { get; protected set; }

    public virtual DateTime UpdatedAt { get; protected set; }
}
