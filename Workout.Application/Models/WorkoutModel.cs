using System.ComponentModel.DataAnnotations;

namespace Workout.Application.Models;

public class WorkoutModel : BaseModel
{
    [Required]
    [MaxLength(64)]
    public string Name { get; set; }

    [MaxLength(2048)]
    public string Description { get; set; }

    public ushort? Duration { get; set; }

    public DifficultyLevelModel? DifficultyLevel { get; set; }

    public uint? DifficultyLevelId { get; set; }

    public List<MovementModel>? Movements { get; set; }

    public uint CreatorId { get; set; }

    public uint UpdaterId { get; set; }
}
