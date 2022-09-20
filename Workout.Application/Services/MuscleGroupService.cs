using AutoMapper;
using Workout.Application.Models;
using Workout.Core.Entities;
using Workout.Core.Querying;
using Workout.Core.Repositories;
using Workout.Infrastructure.Querying;

namespace Workout.Application.Services;

public sealed class MuscleGroupService : IMuscleGroupService
{
    private readonly IMuscleGroupRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public MuscleGroupService(IMuscleGroupRepository repository, IMapper mapper, IUserService userService)
    {
        _repository = repository;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<MuscleGroupModel?> CreateMuscleGroup(MuscleGroupModel muscleGroup)
    {
        var createdMuscleGroup = await _repository.SaveAsync(_mapper.Map<MuscleGroup>(muscleGroup));
        if (createdMuscleGroup != null)
        {
            return _mapper.Map<MuscleGroupModel>(createdMuscleGroup);
        }

        return null;
    }

    public async Task<bool> DeleteMuscleGroupById(uint muscleGroupId)
    {
        return await _repository.DeleteAsync(muscleGroupId);
    }

    public async Task<MuscleGroupModel?> GetMuscleGroupById(uint muscleGroupId)
    {
        var muscleGroup = await _repository.GetByIdAsync(muscleGroupId);
        if (muscleGroup != null)
        {
            return _mapper.Map<MuscleGroupModel>(muscleGroup);
        }

        return null;
    }

    public async Task<IPagedList<MuscleGroupModel>?> GetMuscleGroupList(PagingArgs pagingArgs)
    {
        var muscleGroups = await _repository.ListAllAsync(pagingArgs);
        if (muscleGroups != null)
        {
            var muscleGroupCount = await _repository.CountAsync();
            return new PagedList<MuscleGroupModel>(pagingArgs,
                                                   muscleGroupCount,
                                                   _mapper.Map<IReadOnlyList<MuscleGroupModel>>(muscleGroups));
        }

        return null;
    }
}
