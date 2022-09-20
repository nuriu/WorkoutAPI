using AutoMapper;
using Workout.Application.Models;
using Workout.Core.Entities;
using Workout.Core.Querying;
using Workout.Core.Repositories;
using Workout.Infrastructure.Querying;

namespace Workout.Application.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<uint> AuthenticateUser(string username, string password)
    {
        return await _repository.AuthenticateAsync(new User(username, password));
    }

    public async Task<UserModel?> CreateUser(UserLoginModel user)
    {
        var createdUser = await _repository.SaveAsync(_mapper.Map<User>(user));
        if (createdUser != null)
        {
            return _mapper.Map<UserModel>(createdUser);
        }

        return null;
    }

    public async Task<UserModel?> GetUserById(uint userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user != null)
        {
            return _mapper.Map<UserModel>(user);
        }

        return null;
    }

    public async Task<IPagedList<UserModel>?> GetUserList(PagingArgs pagingArgs)
    {
        var users = await _repository.ListAllAsync(pagingArgs);
        if (users != null)
        {
            var userCount = await _repository.CountAsync();
            return new PagedList<UserModel>(pagingArgs,
                                            userCount,
                                            _mapper.Map<IReadOnlyList<UserModel>>(users));
        }

        return null;
    }
}
