using System.Net;
using System.Text.Json;

namespace Workout.API.Exceptions;

public sealed class WorkoutExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<WorkoutExceptionHandlerMiddleware> _logger;

    public WorkoutExceptionHandlerMiddleware(RequestDelegate next,
                                             ILogger<WorkoutExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception.Message);

        context.Response.ContentType = "application/json";

        var response = context.Response;
        var responseData = "Error!";
        switch (exception)
        {
            case ApplicationException e:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                responseData = e.Message;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                responseData = "Internal server error!";
                break;
        }

        var result = JsonSerializer.Serialize(responseData);
        await context.Response.WriteAsync(result);
    }
}
