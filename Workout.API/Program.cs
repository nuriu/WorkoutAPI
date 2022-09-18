using Workout.API.Authorization;
using Workout.Application.Services;
using Workout.Core.Repositories;
using Workout.Infrastructure.Database;
using Workout.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<IUserService, UserService>();

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

app.MapHealthChecks("/healthcheck");
app.MapControllers();

app.Run();
