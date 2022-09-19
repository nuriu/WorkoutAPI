using System.ComponentModel.DataAnnotations;

namespace Workout.Application.Models;

public class UserLoginModel
{
    [Required]
    [MaxLength(16)]
    public string Username { get; set; }

    [Required]
    [MaxLength(32)]
    public string Password { get; set; }
}

public class UserModel : BaseModel
{
    [MaxLength(16)]
    public string Username { get; set; }
}
