namespace Workout.Core.Entities;

public sealed class Movement : BaseEntity<uint>
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public uint MuscleGroupId { get; set; }
    public uint CreatorId { get; set; }
    public uint UpdaterId { get; set; }

    public Movement(uint id,
                    string name,
                    string? description,
                    uint muscleGroupId,
                    DateTime createdAt,
                    uint creatorId,
                    DateTime updatedAt,
                    uint updaterId)
    {
        Id = id;
        Name = name;
        Description = description;
        MuscleGroupId = muscleGroupId;
        CreatedAt = createdAt;
        CreatorId = creatorId;
        UpdatedAt = updatedAt;
        UpdaterId = updaterId;
    }
}
