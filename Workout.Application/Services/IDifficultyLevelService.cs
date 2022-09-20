using Workout.Application.Models;
using Workout.Core.Querying;

namespace Workout.Application.Services;

/// <summary>
/// Bridge between repository and api.
/// </summary>
public interface IDifficultyLevelService
{
    Task<IPagedList<DifficultyLevelModel>?> GetDifficultyLevelList(PagingArgs pagingArgs);
    Task<DifficultyLevelModel?> GetDifficultyLevelById(uint difficultyLevelId);
    Task<DifficultyLevelModel?> CreateDifficultyLevel(DifficultyLevelModel difficultyLevel);
}
