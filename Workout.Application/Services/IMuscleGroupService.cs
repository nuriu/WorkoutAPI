using Workout.Application.Models;
using Workout.Core.Querying;

namespace Workout.Application.Services;

/// <summary>
/// Bridge between repository and api.
/// </summary>
public interface IMuscleGroupService
{
    Task<IPagedList<MuscleGroupModel>?> GetMuscleGroupList(PagingArgs pagingArgs);
    Task<MuscleGroupModel?> GetMuscleGroupById(uint muscleGroupId);
    Task<MuscleGroupModel?> CreateMuscleGroup(MuscleGroupModel muscleGroup);
    Task<bool> DeleteMuscleGroupById(uint muscleGroupId);
}
