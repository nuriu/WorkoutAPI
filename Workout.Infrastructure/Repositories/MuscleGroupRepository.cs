using MySqlConnector;
using Workout.Core.Entities;
using Workout.Core.Querying;
using Workout.Core.Repositories;
using Workout.Infrastructure.Database;

namespace Workout.Infrastructure.Repositories;

/// <summary>
/// Handles 'MuscleGroup' related database tasks.
/// </summary>
public sealed class MuscleGroupRepository : IMuscleGroupRepository
{
    private readonly IWorkoutDatabase _db;

    public MuscleGroupRepository(IWorkoutDatabase db)
    {
        _db = db;
    }

    public Task<IEnumerable<MuscleGroup>?> AddRangeAsync(IEnumerable<MuscleGroup> entities)
    {
        throw new NotSupportedException();
    }

    public async Task<uint> CountAsync()
    {
        var muscleGroupCount = await _db.CallScalarStoredProcedureAsync(SPList.GET_MUSCLE_GROUP_COUNT,
                                                                        Enumerable.Empty<MySqlParameter>());

        return Convert.ToUInt32(muscleGroupCount);
    }

    public async Task<bool> DeleteAsync(uint id)
    {
        var deletedRowCount = await _db.CallScalarStoredProcedureAsync(SPList.DELETE_MUSCLE_GROUP_BY_ID, new List<MySqlParameter> {
            new MySqlParameter("id", id)
        });

        return Convert.ToInt32(deletedRowCount) > 0;
    }

    public async Task<MuscleGroup?> GetByIdAsync(uint id)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.GET_MUSCLE_GROUP_BY_ID, new List<MySqlParameter> {
            new MySqlParameter("id", id)
        });

        if (reader != null && reader.HasRows)
        {
            MuscleGroup? muscleGroup = null;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    muscleGroup = new MuscleGroup(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                                  reader.GetString(reader.GetOrdinal("name")),
                                                  reader.GetString(reader.GetOrdinal("description")),
                                                  reader.GetDateTime(reader.GetOrdinal("created_at")),
                                                  reader.GetFieldValue<uint>(reader.GetOrdinal("created_by")),
                                                  reader.GetDateTime(reader.GetOrdinal("updated_at")),
                                                  reader.GetFieldValue<uint>(reader.GetOrdinal("updated_by")));
                    if (muscleGroup != null)
                    {
                        break;
                    }
                }
            }
            await _db.Connection.CloseAsync();
            return muscleGroup;
        }

        return null;
    }

    public async Task<IReadOnlyList<MuscleGroup>?> ListAllAsync(PagingArgs pagingArgs)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.GET_MUSCLE_GROUPS_PAGINATED, new List<MySqlParameter> {
            new MySqlParameter("pageIndex", pagingArgs.Index),
            new MySqlParameter("pageSize", pagingArgs.Size)
        });

        if (reader != null && reader.HasRows)
        {
            var muscleGroups = new List<MuscleGroup>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var muscleGroup = new MuscleGroup(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                                      reader.GetString(reader.GetOrdinal("name")),
                                                      reader.GetSafeString(reader.GetOrdinal("description")),
                                                      reader.GetDateTime(reader.GetOrdinal("created_at")),
                                                      reader.GetFieldValue<uint>(reader.GetOrdinal("created_by")),
                                                      reader.GetDateTime(reader.GetOrdinal("updated_at")),
                                                      reader.GetFieldValue<uint>(reader.GetOrdinal("updated_by")));
                    muscleGroups.Add(muscleGroup);
                }
            }
            await _db.Connection.CloseAsync();
            return muscleGroups;
        }

        return null;
    }

    public async Task<MuscleGroup?> SaveAsync(MuscleGroup entity)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.CREATE_MUSCLE_GROUP, new List<MySqlParameter> {
            new MySqlParameter("name", entity.Name),
            new MySqlParameter("description", entity.Description),
            new MySqlParameter("userId", entity.CreatorId),
        });

        if (reader != null && reader.HasRows)
        {
            MuscleGroup? muscleGroup = null;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    muscleGroup = new MuscleGroup(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                                  reader.GetString(reader.GetOrdinal("name")),
                                                  reader.GetString(reader.GetOrdinal("description")),
                                                  reader.GetDateTime(reader.GetOrdinal("created_at")),
                                                  reader.GetFieldValue<uint>(reader.GetOrdinal("created_by")),
                                                  reader.GetDateTime(reader.GetOrdinal("updated_at")),
                                                  reader.GetFieldValue<uint>(reader.GetOrdinal("updated_by")));
                    if (muscleGroup != null)
                    {
                        break;
                    }
                }
            }
            await _db.Connection.CloseAsync();
            return muscleGroup;
        }

        return null;
    }

    public Task<MuscleGroup?> UpdateAsync(uint id, MuscleGroup entity)
    {
        throw new NotSupportedException();
    }
}
