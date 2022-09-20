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

    [Obsolete]
    public Task<IEnumerable<User>?> AddRangeAsync(IEnumerable<User> entities)
    {
        throw new NotSupportedException();
    }

    public async Task<uint> AuthenticateAsync(User user)
    {
        var userId = await _db.CallScalarStoredProcedureAsync(SPList.IS_USER_EXISTS, new List<MySqlParameter> {
            new MySqlParameter("username", user.Username),
            new MySqlParameter("password", user.Password),
        });

        return (userId != null) ? (uint)(userId) : 0;
    }

    public async Task<uint> CountAsync()
    {
        var userCount = await _db.CallScalarStoredProcedureAsync(SPList.GET_USER_COUNT,
                                                                 Enumerable.Empty<MySqlParameter>());

        return Convert.ToUInt32(userCount);
    }

    public async Task<bool> DeleteAsync(uint id)
    {
        var deletedRowCount = await _db.CallScalarStoredProcedureAsync(SPList.DELETE_USER_BY_ID, new List<MySqlParameter> {
            new MySqlParameter("id", id)
        });

        return Convert.ToInt32(deletedRowCount) > 0;
    }

    public async Task<User?> GetByIdAsync(uint id)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.GET_USER_BY_ID, new List<MySqlParameter> {
            new MySqlParameter("id", id)
        });

        if (reader != null && reader.HasRows)
        {
            User? user = null;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    user = new User(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                    reader.GetString(reader.GetOrdinal("username")),
                                    reader.GetString(reader.GetOrdinal("password")),
                                    reader.GetDateTime(reader.GetOrdinal("created_at")),
                                    reader.GetDateTime(reader.GetOrdinal("updated_at")));
                    if (user != null)
                    {
                        break;
                    }
                }
            }
            await _db.Connection.CloseAsync();
            return user;
        }

        return null;
    }

    public async Task<IReadOnlyList<User>?> ListAllAsync(PagingArgs pagingArgs)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.GET_USERS_PAGINATED, new List<MySqlParameter> {
            new MySqlParameter("pageIndex", pagingArgs.Index),
            new MySqlParameter("pageSize", pagingArgs.Size)
        });

        if (reader != null && reader.HasRows)
        {
            var users = new List<User>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var user = new User(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                        reader.GetString(reader.GetOrdinal("username")),
                                        reader.GetString(reader.GetOrdinal("password")),
                                        reader.GetDateTime(reader.GetOrdinal("created_at")),
                                        reader.GetDateTime(reader.GetOrdinal("updated_at")));
                    users.Add(user);
                }
            }
            await _db.Connection.CloseAsync();
            return users;
        }

        return null;
    }

    public async Task<User?> SaveAsync(User entity)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.CREATE_USER, new List<MySqlParameter> {
            new MySqlParameter("username", entity.Username),
            new MySqlParameter("password", entity.Password)
        });

        if (reader != null && reader.HasRows)
        {
            User? user = null;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    user = new User(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                    reader.GetString(reader.GetOrdinal("username")),
                                    reader.GetString(reader.GetOrdinal("password")),
                                    reader.GetDateTime(reader.GetOrdinal("created_at")),
                                    reader.GetDateTime(reader.GetOrdinal("updated_at")));
                    if (user != null)
                    {
                        break;
                    }
                }
            }
            await _db.Connection.CloseAsync();
            return user;
        }

        return null;
    }

    public Task<User?> UpdateAsync(uint id, User entity)
    {
        throw new NotSupportedException();
    }
}
