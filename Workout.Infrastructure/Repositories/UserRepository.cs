using MySqlConnector;
using Workout.Core.Entities;
using Workout.Core.Querying;
using Workout.Core.Repositories;
using Workout.Infrastructure.Database;

namespace Workout.Infrastructure.Repositories;

/// <summary>
/// Handles 'User' related database tasks.
/// </summary>
public sealed class UserRepository : IUserRepository
{
    private readonly IWorkoutDatabase _db;

    public UserRepository(IWorkoutDatabase db)
    {
        _db = db;
    }

    public Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> entities)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AuthenticateAsync(User user)
    {
        var isUserExists = await _db.CallScalarStoredProcedureAsync(SPList.IS_USER_EXISTS, new List<MySqlParameter> {
            new MySqlParameter("username", user.Username),
            new MySqlParameter("password", user.Password),
        });

        return Convert.ToBoolean(isUserExists);
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<User>> ListAllAsync(PagingArgs pagingArgs)
    {
        throw new NotImplementedException();
    }

    public Task<User> SaveAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> SearchUsersAsync(PagingArgs pagingArgs, SearchArgs searchArgs)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(int id, User entity)
    {
        throw new NotImplementedException();
    }
}
