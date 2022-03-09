using ApplicationUser.Infrastructures;
using ApplicationUser.Infrastructures.Installed;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VietBND.AspNetCore.Exceptions;
using VietBND.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http.Headers;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container => container.AddVietBNDFramework());
builder.Host.ConfigureContainer<ContainerBuilder>(container => container.AddServices());
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.Load("ApplicationUser.Infrastructures"));
builder.Services.AddMediatR(Assembly.Load("ApplicationUser.Infrastructures"));
Console.WriteLine(Assembly.Load("ApplicationUser.Infrastructures"));
//builder.Services.AddGrpc();
builder.Services.AddValidatorsFromAssembly(Assembly.Load("ApplicationUser.Infrastructures"));

builder.Services.AddDbContext<ApplicationUserDbContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var configuration = builder.Configuration
    .AddJsonFile($"appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
    .AddEnvironmentVariables()
    .Build();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }).AddOAuth("ShoppingCart", options =>
    {
        options.ClientId = configuration["OAuth:ClientId"];
        options.ClientSecret = configuration["OAuth:ClientSecret"];
        options.Scope.Add("applicationuser");
        options.SaveTokens = true;
        options.TokenEndpoint = "http://shoppingcart-identity-api/api/auth/login";
        options.UserInformationEndpoint = "http://shoppingcart-identity-api/api/auth/get-info";

        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                // Get user info from the userinfo endpoint and use it to populate user claims
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                response.EnsureSuccessStatusCode();

                var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

                context.RunClaimActions(user.RootElement);
            }
        };
    });


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

//app.UseMiddleware<JwtMiddleware>();


app.MapControllers();

app.Run();
