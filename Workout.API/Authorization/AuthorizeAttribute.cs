using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Workout.API.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var hasAllowAnonymousAttribute = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (hasAllowAnonymousAttribute)
            return;

        var user = Convert.ToBoolean(context.HttpContext.Items["User"]);
        if (user == false)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
        }
    }
}
