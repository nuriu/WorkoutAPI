using System.Diagnostics;
using Serilog;
using Serilog.Events;
using Workout.API.Authorization;
using Workout.API.Exceptions;
using Workout.Application.Services;
using Workout.Core.Repositories;
using Workout.Infrastructure.Database;
using Workout.Infrastructure.Repositories;

Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
Serilog.Debugging.SelfLog.Enable(Console.Error);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/workout.txt", restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day)
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

if (Environment.GetEnvironmentVariable("DB_HOST") == null
    || Environment.GetEnvironmentVariable("DB_USER") == null
    || Environment.GetEnvironmentVariable("DB_PASS") == null
    || Environment.GetEnvironmentVariable("DB_PORT") == null
    || Environment.GetEnvironmentVariable("DB_NAME") == null)
{
    throw new Exception("Couldn't find required variables at Environment.");
}

var connectionString = String.Format("Server={0};User ID={1};Password={2};Port={3};Database={4}",
                                     Environment.GetEnvironmentVariable("DB_HOST"),
                                     Environment.GetEnvironmentVariable("DB_USER"),
                                     Environment.GetEnvironmentVariable("DB_PASS"),
                                     Environment.GetEnvironmentVariable("DB_PORT"),
                                     Environment.GetEnvironmentVariable("DB_NAME"));

builder.Services.AddTransient<IWorkoutDatabase>(_ => new WorkoutMySQLDatabase(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDifficultyLevelRepository, DifficultyLevelRepository>();
builder.Services.AddScoped<IMuscleGroupRepository, MuscleGroupRepository>();
builder.Services.AddScoped<IMovementRepository, MovementRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDifficultyLevelService, DifficultyLevelService>();
builder.Services.AddScoped<IMuscleGroupService, MuscleGroupService>();
builder.Services.AddScoped<IMovementService, MovementService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<BasicAuthenticationMiddleware>();
app.UseMiddleware<WorkoutExceptionHandlerMiddleware>();

app.MapHealthChecks("/healthcheck");
app.MapControllers();

app.Run();
