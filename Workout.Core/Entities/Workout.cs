namespace Workout.Core.Entities;

public sealed class Workout : BaseEntity<uint>
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public ushort? Duration { get; set; }

    public DifficultyLevel? DifficultyLevel { get; set; }
    public uint? DifficultyLevelId { get; set; }

    public uint CreatorId { get; set; }
    public uint UpdaterId { get; set; }

    public Workout(uint id,
                   string name,
                   string? description,
                   ushort? duration,
                   uint? difficultyLevelId,
                   DateTime createdAt,
                   uint creatorId,
                   DateTime updatedAt,
                   uint updaterId)
    {
        Id = id;
        Name = name;
        Description = description;
        Duration = duration;
        DifficultyLevelId = difficultyLevelId;
        CreatedAt = createdAt;
        CreatorId = creatorId;
        UpdatedAt = updatedAt;
        UpdaterId = updaterId;
    }
}
