namespace Workout.Core.Entities;

public sealed class User : BaseEntity<uint>
{
    public string Username { get; set; }
    public string Password { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public User(uint id,
                string username,
                string password,
                DateTime createdAt,
                DateTime updatedAt)
    {
        Id = id;
        Username = username;
        Password = password;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
