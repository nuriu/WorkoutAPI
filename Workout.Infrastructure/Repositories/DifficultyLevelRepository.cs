using MySqlConnector;
using Workout.Core.Entities;
using Workout.Core.Querying;
using Workout.Core.Repositories;
using Workout.Infrastructure.Database;

namespace Workout.Infrastructure.Repositories;

/// <summary>
/// Handles 'DifficultyLevel' related database tasks.
/// </summary>
public sealed class DifficultyLevelRepository : IDifficultyLevelRepository
{
    private readonly IWorkoutDatabase _db;

    public DifficultyLevelRepository(IWorkoutDatabase db)
    {
        _db = db;
    }

    public Task<IEnumerable<DifficultyLevel>?> AddRangeAsync(IEnumerable<DifficultyLevel> entities)
    {
        throw new NotSupportedException();
    }

    public async Task<uint> CountAsync()
    {
        var difficultyLevelCount = await _db.CallScalarStoredProcedureAsync(SPList.GET_DIFFICULTY_LEVEL_COUNT,
                                                                            Enumerable.Empty<MySqlParameter>());

        return Convert.ToUInt32(difficultyLevelCount);
    }

    public async Task<bool> DeleteAsync(uint id)
    {
        var deletedRowCount = await _db.CallScalarStoredProcedureAsync(SPList.DELETE_DIFFICULTY_LEVEL_BY_ID, new List<MySqlParameter> {
            new MySqlParameter("id", id)
        });

        return Convert.ToInt32(deletedRowCount) > 0;
    }

    public async Task<DifficultyLevel?> GetByIdAsync(uint id)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.GET_DIFFICULTY_LEVEL_BY_ID, new List<MySqlParameter> {
            new MySqlParameter("id", id)
        });

        if (reader != null && reader.HasRows)
        {
            DifficultyLevel? difficultyLevel = null;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    difficultyLevel = new DifficultyLevel(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                                          reader.GetString(reader.GetOrdinal("name")),
                                                          reader.GetString(reader.GetOrdinal("description")),
                                                          reader.GetDateTime(reader.GetOrdinal("created_at")),
                                                          reader.GetFieldValue<uint>(reader.GetOrdinal("created_by")),
                                                          reader.GetDateTime(reader.GetOrdinal("updated_at")),
                                                          reader.GetFieldValue<uint>(reader.GetOrdinal("updated_by")));
                    if (difficultyLevel != null)
                    {
                        break;
                    }
                }
            }
            await _db.Connection.CloseAsync();
            return difficultyLevel;
        }

        return null;
    }

    public async Task<IReadOnlyList<DifficultyLevel>?> ListAllAsync(PagingArgs pagingArgs)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.GET_DIFFICULTY_LEVELS_PAGINATED, new List<MySqlParameter> {
            new MySqlParameter("pageIndex", pagingArgs.Index),
            new MySqlParameter("pageSize", pagingArgs.Size)
        });

        if (reader != null && reader.HasRows)
        {
            var difficultyLevels = new List<DifficultyLevel>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var difficultyLevel = new DifficultyLevel(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                                              reader.GetString(reader.GetOrdinal("name")),
                                                              reader.GetString(reader.GetOrdinal("description")),
                                                              reader.GetDateTime(reader.GetOrdinal("created_at")),
                                                              reader.GetFieldValue<uint>(reader.GetOrdinal("created_by")),
                                                              reader.GetDateTime(reader.GetOrdinal("updated_at")),
                                                              reader.GetFieldValue<uint>(reader.GetOrdinal("updated_by")));
                    difficultyLevels.Add(difficultyLevel);
                }
            }
            await _db.Connection.CloseAsync();
            return difficultyLevels;
        }

        return null;
    }

    public async Task<DifficultyLevel?> SaveAsync(DifficultyLevel entity)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.CREATE_DIFFICULTY_LEVEL, new List<MySqlParameter> {
            new MySqlParameter("name", entity.Name),
            new MySqlParameter("description", entity.Description),
            new MySqlParameter("userId", entity.CreatedById),
        });

        if (reader != null && reader.HasRows)
        {
            DifficultyLevel? difficultyLevel = null;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    difficultyLevel = new DifficultyLevel(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                                          reader.GetString(reader.GetOrdinal("name")),
                                                          reader.GetString(reader.GetOrdinal("description")),
                                                          reader.GetDateTime(reader.GetOrdinal("created_at")),
                                                          reader.GetFieldValue<uint>(reader.GetOrdinal("created_by")),
                                                          reader.GetDateTime(reader.GetOrdinal("updated_at")),
                                                          reader.GetFieldValue<uint>(reader.GetOrdinal("updated_by")));
                    if (difficultyLevel != null)
                    {
                        break;
                    }
                }
            }
            await _db.Connection.CloseAsync();
            return difficultyLevel;
        }

        return null;
    }

    public Task<DifficultyLevel?> UpdateAsync(uint id, DifficultyLevel entity)
    {
        throw new NotSupportedException();
    }
}
