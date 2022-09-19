using Workout.Application.Models;
using Workout.Core.Entities;
using Workout.Core.Querying;
using Workout.Core.Repositories;
using Workout.Infrastructure.Querying;

namespace Workout.Application.Services;

// TODO: setup mapper
public sealed class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> AuthenticateUser(string username, string password)
    {
        return await _repository.AuthenticateAsync(new User(username, password));
    }

    public async Task<UserModel?> CreateUser(UserModel user)
    {
        var createdUser = await _repository.SaveAsync(new User(user.Username, user.Password));
        if (createdUser != null)
        {
            return new UserModel(createdUser.Id, createdUser.Username, string.Empty, createdUser.CreatedAt, createdUser.UpdatedAt);
        }

        return null;
    }

    public async Task<UserModel?> GetUserById(int userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user != null)
        {
            return new UserModel(user.Id, user.Username, string.Empty, user.CreatedAt, user.UpdatedAt);
        }

        return null;
    }

    public async Task<IPagedList<UserModel>?> GetUserList(PagingArgs pagingArgs)
    {
        var users = await _repository.ListAllAsync(pagingArgs);
        if (users != null)
        {
            return new PagedList<UserModel>(pagingArgs, users.Select(x => new UserModel(x.Id, x.Username, string.Empty, x.CreatedAt, x.UpdatedAt)));
        }

        return null;
    }
}
