using AutoMapper;
using Workout.Application.Models;
using Workout.Core.Entities;
using Workout.Core.Querying;
using Workout.Core.Repositories;
using Workout.Infrastructure.Querying;

namespace Workout.Application.Services;

public sealed class MovementService : IMovementService
{
    private readonly IMovementRepository _repository;
    private readonly IMapper _mapper;

    public MovementService(IMovementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MovementModel?> CreateMovement(MovementModel movement)
    {
        var createdMovement = await _repository.SaveAsync(_mapper.Map<Movement>(movement));
        if (createdMovement != null)
        {
            return _mapper.Map<MovementModel>(createdMovement);
        }

        return null;
    }

    public async Task<bool> DeleteMovementById(uint movementId)
    {
        return await _repository.DeleteAsync(movementId);
    }

    public async Task<MovementModel?> GetMovementById(uint movementId)
    {
        var movement = await _repository.GetByIdAsync(movementId);
        if (movement != null)
        {
            return _mapper.Map<MovementModel>(movement);
        }

        return null;
    }

    public async Task<IPagedList<MovementModel>?> GetMovementList(PagingArgs pagingArgs)
    {
        var movements = await _repository.ListAllAsync(pagingArgs);
        if (movements != null)
        {
            var movementCount = await _repository.CountAsync();
            return new PagedList<MovementModel>(pagingArgs,
                                                movementCount,
                                                _mapper.Map<IReadOnlyList<MovementModel>>(movements));
        }

        return null;
    }
}
