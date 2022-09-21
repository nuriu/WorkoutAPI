using AutoMapper;
using Workout.Application.Models;
using Workout.Core.Querying;
using Workout.Core.Repositories;
using Workout.Infrastructure.Querying;

namespace Workout.Application.Services;

public sealed class WorkoutService : IWorkoutService
{
    private readonly IWorkoutRepository _repository;
    private readonly IMapper _mapper;

    public WorkoutService(IWorkoutRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<WorkoutModel?> AddMovementToWorkout(uint workoutId, uint movementId)
    {
        var workout = await _repository.AddMovementToWorkoutAsync(workoutId, movementId);
        if (workout != null)
        {
            return _mapper.Map<WorkoutModel>(workout);
        }

        return null;
    }

    public async Task<WorkoutModel?> CreateWorkout(WorkoutModel workout)
    {
        var createdWorkout = await _repository.SaveAsync(_mapper.Map<Core.Entities.Workout>(workout));
        if (createdWorkout != null)
        {
            return _mapper.Map<WorkoutModel>(createdWorkout);
        }

        return null;
    }

    public async Task<bool> DeleteWorkoutById(uint workoutId)
    {
        return await _repository.DeleteAsync(workoutId);
    }

    public async Task<WorkoutModel?> GetWorkoutById(uint workoutId)
    {
        var workout = await _repository.GetByIdAsync(workoutId);
        if (workout != null)
        {
            return _mapper.Map<WorkoutModel>(workout);
        }

        return null;
    }

    public async Task<IPagedList<WorkoutModel>?> GetWorkoutList(PagingArgs pagingArgs)
    {
        var workouts = await _repository.ListAllAsync(pagingArgs);
        if (workouts != null)
        {
            var workoutCount = await _repository.CountAsync();
            return new PagedList<WorkoutModel>(pagingArgs,
                                               workoutCount,
                                               _mapper.Map<IReadOnlyList<WorkoutModel>>(workouts));
        }

        return null;
    }

    public async Task<WorkoutModel?> RemoveMovementFromWorkout(uint workoutId, uint movementId)
    {
        var workout = await _repository.RemoveMovementFromWorkoutAsync(workoutId, movementId);
        if (workout != null)
        {
            return _mapper.Map<WorkoutModel>(workout);
        }

        return null;
    }
}
