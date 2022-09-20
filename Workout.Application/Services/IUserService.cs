using Workout.Application.Models;
using Workout.Core.Querying;

namespace Workout.Application.Services;

/// <summary>
/// Bridge between repository and api.
/// </summary>
public interface IUserService
{
    Task<uint> AuthenticateUser(string username, string password);
    Task<IPagedList<UserModel>?> GetUserList(PagingArgs pagingArgs);
    Task<UserModel?> GetUserById(uint userId);
    Task<UserModel?> CreateUser(UserLoginModel user);
}
