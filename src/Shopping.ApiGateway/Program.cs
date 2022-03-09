using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
    .AddJsonFile($"configurations.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();
