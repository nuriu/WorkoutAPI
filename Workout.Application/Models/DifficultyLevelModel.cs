using System.ComponentModel.DataAnnotations;

namespace Workout.Application.Models;

public class DifficultyLevelModel : BaseModel
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    [MaxLength(128)]
    public string Description { get; set; }

    public UserModel CreatedBy { get; set; }

    public UserModel UpdatedBy { get; set; }
}
