using System.Net.Http.Headers;
using System.Text;
using Workout.Application.Services;

namespace Workout.API.Authorization;

public sealed class BasicAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public BasicAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        try
        {
            var authenticationHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authenticationHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

            context.Items["User"] = await userService.AuthenticateUser(credentials[0], credentials[1]);
        }
        catch
        {
            // user is not authenticated
        }

        await _next(context);
    }
}
