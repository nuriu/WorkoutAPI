namespace Workout.Core.Entities;

public class User : BaseEntity<int>
{
    public string Username { get; set; }
    public string Password { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public User(int id, string username, string password, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Username = username;
        Password = password;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
