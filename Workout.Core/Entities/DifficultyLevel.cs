namespace Workout.Core.Entities;

public sealed class DifficultyLevel : BaseEntity<uint>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public uint CreatedById { get; set; }
    public uint UpdatedById { get; set; }

    public DifficultyLevel(uint id,
                           string name,
                           string description,
                           DateTime createdAt,
                           uint createdBy,
                           DateTime updatedAt,
                           uint updatedBy)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        CreatedById = createdBy;
        UpdatedAt = updatedAt;
        UpdatedById = updatedBy;
    }
}
