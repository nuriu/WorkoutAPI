namespace Workout.Core.Entities;

public sealed class MuscleGroup : BaseEntity<uint>
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public uint CreatorId { get; set; }
    public uint UpdaterId { get; set; }

    public MuscleGroup(uint id,
                       string name,
                       string? description,
                       DateTime createdAt,
                       uint creatorId,
                       DateTime updatedAt,
                       uint updaterId)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        CreatorId = creatorId;
        UpdatedAt = updatedAt;
        UpdaterId = updaterId;
    }
}
