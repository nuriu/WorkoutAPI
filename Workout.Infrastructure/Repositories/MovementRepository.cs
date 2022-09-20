using MySqlConnector;
using Workout.Core.Entities;
using Workout.Core.Querying;
using Workout.Core.Repositories;
using Workout.Infrastructure.Database;

namespace Workout.Infrastructure.Repositories;

/// <summary>
/// Handles 'Movement' related database tasks.
/// </summary>
public sealed class MovementRepository : IMovementRepository
{
    private readonly IWorkoutDatabase _db;
    private readonly IMuscleGroupRepository _muscleGroupRepository;

    public MovementRepository(IWorkoutDatabase db, IMuscleGroupRepository muscleGroupRepository)
    {
        _db = db;
        _muscleGroupRepository = muscleGroupRepository;
    }

    public Task<IEnumerable<Movement>?> AddRangeAsync(IEnumerable<Movement> entities)
    {
        throw new NotSupportedException();
    }

    public async Task<uint> CountAsync()
    {
        var movementCount = await _db.CallScalarStoredProcedureAsync(SPList.GET_MOVEMENT_COUNT,
                                                                     Enumerable.Empty<MySqlParameter>());

        return Convert.ToUInt32(movementCount);
    }

    public async Task<bool> DeleteAsync(uint id)
    {
        var deletedRowCount = await _db.CallScalarStoredProcedureAsync(SPList.DELETE_MOVEMENT_BY_ID, new List<MySqlParameter> {
            new MySqlParameter("id", id)
        });

        return Convert.ToInt32(deletedRowCount) > 0;
    }

    public async Task<Movement?> GetByIdAsync(uint id)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.GET_MOVEMENT_BY_ID, new List<MySqlParameter> {
            new MySqlParameter("id", id)
        });

        if (reader != null && reader.HasRows)
        {
            Movement? movement = null;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    movement = new Movement(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                            reader.GetString(reader.GetOrdinal("name")),
                                            reader.GetSafeString(reader.GetOrdinal("description")),
                                            reader.GetFieldValue<uint>(reader.GetOrdinal("muscle_group_id")),
                                            reader.GetDateTime(reader.GetOrdinal("created_at")),
                                            reader.GetFieldValue<uint>(reader.GetOrdinal("created_by")),
                                            reader.GetDateTime(reader.GetOrdinal("updated_at")),
                                            reader.GetFieldValue<uint>(reader.GetOrdinal("updated_by")));

                    movement.MuscleGroup = await _muscleGroupRepository.GetByIdAsync(movement.MuscleGroupId);

                    if (movement != null)
                    {
                        break;
                    }
                }
            }
            await _db.Connection.CloseAsync();
            return movement;
        }

        return null;
    }

    public async Task<IReadOnlyList<Movement>?> ListAllAsync(PagingArgs pagingArgs)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.GET_MOVEMENTS_PAGINATED, new List<MySqlParameter> {
            new MySqlParameter("pageIndex", pagingArgs.Index),
            new MySqlParameter("pageSize", pagingArgs.Size)
        });

        if (reader != null && reader.HasRows)
        {
            var movements = new List<Movement>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var movement = new Movement(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                                reader.GetString(reader.GetOrdinal("name")),
                                                reader.GetSafeString(reader.GetOrdinal("description")),
                                                reader.GetFieldValue<uint>(reader.GetOrdinal("muscle_group_id")),
                                                reader.GetDateTime(reader.GetOrdinal("created_at")),
                                                reader.GetFieldValue<uint>(reader.GetOrdinal("created_by")),
                                                reader.GetDateTime(reader.GetOrdinal("updated_at")),
                                                reader.GetFieldValue<uint>(reader.GetOrdinal("updated_by")));
                    movement.MuscleGroup = await _muscleGroupRepository.GetByIdAsync(movement.MuscleGroupId);
                    movements.Add(movement);
                }
            }
            await _db.Connection.CloseAsync();
            return movements;
        }

        return null;
    }

    public async Task<Movement?> SaveAsync(Movement entity)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.CREATE_MOVEMENT, new List<MySqlParameter> {
            new MySqlParameter("name", entity.Name),
            new MySqlParameter("description", entity.Description),
            new MySqlParameter("muscle_group_id", entity.MuscleGroupId),
            new MySqlParameter("userId", entity.CreatorId),
        });

        if (reader != null && reader.HasRows)
        {
            Movement? movement = null;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    movement = new Movement(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                            reader.GetString(reader.GetOrdinal("name")),
                                            reader.GetSafeString(reader.GetOrdinal("description")),
                                            reader.GetFieldValue<uint>(reader.GetOrdinal("muscle_group_id")),
                                            reader.GetDateTime(reader.GetOrdinal("created_at")),
                                            reader.GetFieldValue<uint>(reader.GetOrdinal("created_by")),
                                            reader.GetDateTime(reader.GetOrdinal("updated_at")),
                                            reader.GetFieldValue<uint>(reader.GetOrdinal("updated_by")));
                    if (movement != null)
                    {
                        break;
                    }
                }
            }
            await _db.Connection.CloseAsync();
            return movement;
        }

        return null;
    }

    public Task<Movement?> UpdateAsync(uint id, Movement entity)
    {
        throw new NotSupportedException();
    }
}
