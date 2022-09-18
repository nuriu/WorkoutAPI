namespace Workout.Application.Models;

public class UserModel : BaseModel
{
    public string Username { get; set; }
    public string Password { get; set; }

    public UserModel(int id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }
}
