using Workout.Application.Models;
using Workout.Core.Querying;

namespace Workout.Application.Services;

/// <summary>
/// Bridge between repository and api.
/// </summary>
public interface IUserService
{
    Task<bool> AuthenticateUser(string username, string password);
    Task<IPagedList<UserModel>?> GetUserList(PagingArgs pagingArgs);
    Task<UserModel?> GetUserById(int userId);
    Task<UserModel?> CreateUser(UserModel user);
}
