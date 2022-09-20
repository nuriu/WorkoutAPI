using System.ComponentModel.DataAnnotations;

namespace Workout.Application.Models;

public class MuscleGroupModel : BaseModel
{
    [Required]
    [MaxLength(64)]
    public string Name { get; set; }

    [MaxLength(512)]
    public string Description { get; set; }

    public uint CreatorId { get; set; }

    public uint UpdaterId { get; set; }
}
