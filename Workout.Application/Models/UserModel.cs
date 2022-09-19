using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Workout.Application.Models;

public class UserModel : BaseModel
{
    [Required]
    [MaxLength(16)]
    public string Username { get; set; }

    [Required]
    [MaxLength(32)]
    public string Password { get; set; }

    [JsonConstructor]
    public UserModel(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public UserModel(int id, string username, string password, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Username = username;
        Password = password;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
