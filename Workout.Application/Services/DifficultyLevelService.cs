using AutoMapper;
using Workout.Application.Models;
using Workout.Core.Entities;
using Workout.Core.Querying;
using Workout.Core.Repositories;
using Workout.Infrastructure.Querying;

namespace Workout.Application.Services;

public sealed class DifficultyLevelService : IDifficultyLevelService
{
    private readonly IDifficultyLevelRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public DifficultyLevelService(IDifficultyLevelRepository repository, IMapper mapper, IUserService userService)
    {
        _repository = repository;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<DifficultyLevelModel?> CreateDifficultyLevel(DifficultyLevelModel difficultyLevel)
    {
        var createdDifficultyLevel = await _repository.SaveAsync(_mapper.Map<DifficultyLevel>(difficultyLevel));
        if (createdDifficultyLevel != null)
        {
            return _mapper.Map<DifficultyLevelModel>(createdDifficultyLevel);
        }

        return null;
    }

    public async Task<DifficultyLevelModel?> GetDifficultyLevelById(uint difficultyLevelId)
    {
        var difficultyLevel = await _repository.GetByIdAsync(difficultyLevelId);
        if (difficultyLevel != null)
        {
            return _mapper.Map<DifficultyLevelModel>(difficultyLevel);
        }

        return null;
    }

    public async Task<IPagedList<DifficultyLevelModel>?> GetDifficultyLevelList(PagingArgs pagingArgs)
    {
        var difficultyLevels = await _repository.ListAllAsync(pagingArgs);
        if (difficultyLevels != null)
        {
            var difficultyLevelCount = await _repository.CountAsync();
            return new PagedList<DifficultyLevelModel>(pagingArgs,
                                                       difficultyLevelCount,
                                                       _mapper.Map<IReadOnlyList<DifficultyLevelModel>>(difficultyLevels));
        }

        return null;
    }
}
