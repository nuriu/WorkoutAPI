using System.Data.Common;
using MySqlConnector;
using Workout.Core.Querying;
using Workout.Core.Repositories;
using Workout.Infrastructure.Database;

namespace Workout.Infrastructure.Repositories;

/// <summary>
/// Handles 'Workout' related database tasks.
/// </summary>
public sealed class WorkoutRepository : IWorkoutRepository
{
    private readonly IWorkoutDatabase _db;
    private readonly IMovementRepository _movementRepository;
    private readonly IDifficultyLevelRepository _difficultyLevelRepository;

    public WorkoutRepository(IWorkoutDatabase db,
                             IMovementRepository movementRepository,
                             IDifficultyLevelRepository difficultyLevelRepository)
    {
        _db = db;
        _movementRepository = movementRepository;
        _difficultyLevelRepository = difficultyLevelRepository;
    }

    public async Task<Core.Entities.Workout?> AddMovementToWorkoutAsync(uint workoutId, uint movementId)
    {
        await _db.CallScalarStoredProcedureAsync(SPList.ADD_MOVEMENT_TO_WORKOUT, new List<MySqlParameter> {
            new MySqlParameter("workoutId", workoutId),
            new MySqlParameter("movementId", movementId)
        });

        return await GetByIdAsync(workoutId);
    }

    public Task<IEnumerable<Core.Entities.Workout>?> AddRangeAsync(IEnumerable<Core.Entities.Workout> entities)
    {
        throw new NotSupportedException();
    }

    public async Task<uint> CountAsync()
    {
        var workoutCount = await _db.CallScalarStoredProcedureAsync(SPList.GET_WORKOUT_COUNT,
                                                                    Enumerable.Empty<MySqlParameter>());

        return Convert.ToUInt32(workoutCount);
    }

    public async Task<bool> DeleteAsync(uint id)
    {
        var deletedRowCount = await _db.CallScalarStoredProcedureAsync(SPList.DELETE_WORKOUT_BY_ID, new List<MySqlParameter> {
            new MySqlParameter("id", id)
        });

        return Convert.ToInt32(deletedRowCount) > 0;
    }

    public async Task<Core.Entities.Workout?> GetByIdAsync(uint id)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.GET_WORKOUT_BY_ID, new List<MySqlParameter> {
            new MySqlParameter("id", id)
        });

        if (reader != null && reader.HasRows)
        {
            Core.Entities.Workout? workout = null;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    workout = ConstructWorkoutFrom(reader);
                    if (workout != null)
                    {
                        if (workout.DifficultyLevelId != null)
                        {
                            workout.DifficultyLevel = await _difficultyLevelRepository.GetByIdAsync((uint)workout.DifficultyLevelId);
                            workout.Movements = await _movementRepository.GetMovementsByWorkoutIdAsync(workout.Id);
                        }
                        break;
                    }
                }
            }
            await _db.Connection.CloseAsync();
            return workout;
        }

        return null;
    }

    public async Task<IReadOnlyList<Core.Entities.Workout>?> ListAllAsync(PagingArgs pagingArgs)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.GET_WORKOUTS_PAGINATED, new List<MySqlParameter> {
            new MySqlParameter("pageIndex", pagingArgs.Index),
            new MySqlParameter("pageSize", pagingArgs.Size)
        });

        if (reader != null && reader.HasRows)
        {
            var workouts = new List<Core.Entities.Workout>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var workout = ConstructWorkoutFrom(reader);
                    if (workout != null && workout.DifficultyLevelId != null)
                    {
                        workout.DifficultyLevel = await _difficultyLevelRepository.GetByIdAsync((uint)workout.DifficultyLevelId);
                        workout.Movements = await _movementRepository.GetMovementsByWorkoutIdAsync(workout.Id);
                    }
                    workouts.Add(workout);
                }
            }
            await _db.Connection.CloseAsync();
            return workouts;
        }

        return null;
    }

    public async Task<Core.Entities.Workout?> RemoveMovementFromWorkoutAsync(uint workoutId, uint movementId)
    {
        await _db.CallScalarStoredProcedureAsync(SPList.REMOVE_MOVEMENT_FROM_WORKOUT, new List<MySqlParameter> {
            new MySqlParameter("workoutId", workoutId),
            new MySqlParameter("movementId", movementId)
        });

        return await GetByIdAsync(workoutId);
    }

    public async Task<Core.Entities.Workout?> SaveAsync(Core.Entities.Workout entity)
    {
        await _db.Connection.OpenAsync();
        var reader = await _db.CallStoredProcedureAsync(SPList.CREATE_WORKOUT, new List<MySqlParameter> {
            new MySqlParameter("name", entity.Name),
            new MySqlParameter("description", entity.Description),
            new MySqlParameter("duration", entity.Duration),
            new MySqlParameter("difficulty_level_id", entity.DifficultyLevelId),
            new MySqlParameter("userId", entity.CreatorId),
        });

        if (reader != null && reader.HasRows)
        {
            Core.Entities.Workout? workout = null;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    workout = ConstructWorkoutFrom(reader);
                    if (workout != null && workout.DifficultyLevelId != null)
                    {
                        workout.DifficultyLevel = await _difficultyLevelRepository.GetByIdAsync((uint)workout.DifficultyLevelId);
                        workout.Movements = await _movementRepository.GetMovementsByWorkoutIdAsync(workout.Id);
                    }
                    if (workout != null)
                    {
                        break;
                    }
                }
            }
            await _db.Connection.CloseAsync();
            return workout;
        }

        return null;
    }

    public Task<Core.Entities.Workout?> UpdateAsync(uint id, Core.Entities.Workout entity)
    {
        throw new NotSupportedException();
    }

    private Core.Entities.Workout? ConstructWorkoutFrom(DbDataReader reader)
    {
        if (reader != null && reader.HasRows)
        {
            return new Core.Entities.Workout(reader.GetFieldValue<uint>(reader.GetOrdinal("id")),
                                             reader.GetString(reader.GetOrdinal("name")),
                                             reader.GetSafeString(reader.GetOrdinal("description")),
                                             reader.GetSafeFieldValue<ushort>(reader.GetOrdinal("duration")),
                                             reader.GetSafeFieldValue<uint>(reader.GetOrdinal("difficulty_level_id")),
                                             reader.GetDateTime(reader.GetOrdinal("created_at")),
                                             reader.GetFieldValue<uint>(reader.GetOrdinal("created_by")),
                                             reader.GetDateTime(reader.GetOrdinal("updated_at")),
                                             reader.GetFieldValue<uint>(reader.GetOrdinal("updated_by")));
        }

        return null;
    }
}
