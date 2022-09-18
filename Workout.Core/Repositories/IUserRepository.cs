using Workout.Core.Entities;
using Workout.Core.Querying;

namespace Workout.Core.Repositories;

public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Checks if user exists in the database.
    /// </summary>
    /// <param name="user">User entity to perform check on.</param>
    /// <returns>A boolean representing given user's existence.</returns>
    Task<bool> AuthenticateAsync(User user);
    Task<User> SearchUsersAsync(PagingArgs pagingArgs, SearchArgs searchArgs);
}
