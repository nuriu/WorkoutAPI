using Microsoft.AspNetCore.Mvc;

namespace WorkoutAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public Dictionary<string, string?> Get()
    {
        return new Dictionary<string, string?>{
            { "ASPNETCORE_ENVIRONMENT", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") },
            { "DB_HOST", Environment.GetEnvironmentVariable("DB_HOST") },
            { "DB_PORT", Environment.GetEnvironmentVariable("DB_PORT") },
            { "DB_NAME", Environment.GetEnvironmentVariable("DB_NAME") },
            { "DB_USER", Environment.GetEnvironmentVariable("DB_USER") },
            { "DB_PASS", Environment.GetEnvironmentVariable("DB_PASS") }
        };
    }
}
