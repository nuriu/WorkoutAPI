using Workout.Application.Models;
using Workout.Core.Entities;
using Workout.Core.Querying;
using Workout.Core.Repositories;

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

    public async Task<UserModel> CreateUser(UserModel user)
    {
        var createdUser = await _repository.SaveAsync(new User(user.Username, user.Password));
        return new UserModel(createdUser.Id, createdUser.Username, createdUser.Password);
    }

    public async Task<bool> DeleteUserById(int userId)
    {
        return await _repository.DeleteAsync(userId);
    }

    public async Task<UserModel> GetUserById(int userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        return new UserModel(user.Id, user.Username, user.Password);
    }

    public async Task<IPagedList<UserModel>> GetUserList(PagingArgs pagingArgs)
    {
        var users = await _repository.ListAllAsync(pagingArgs);
        // return new PagedList<UserModel>(pagingArgs, users);

        throw new NotImplementedException();
    }

    public Task<IPagedList<UserModel>> SearchUsers(PagingArgs pagingArgs, SearchArgs searchArgs)
    {
        throw new NotImplementedException();
    }

    public async Task<UserModel> UpdateUser(int userId, UserModel user)
    {
        var updatedUser = await _repository.UpdateAsync(userId, new User(user.Username, user.Password));
        return new UserModel(updatedUser.Id, updatedUser.Username, updatedUser.Password);
    }
}
