using System.ComponentModel.DataAnnotations;

namespace Workout.Application.Models;

public class UserModel : BaseModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public UserModel(int id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }
}
