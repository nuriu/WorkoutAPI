using System.ComponentModel.DataAnnotations;

namespace Workout.Application.Models;

public class UserModel : BaseModel
{
    [MaxLength(16)]
    public string Username { get; set; }
}
