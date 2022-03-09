using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using Identity.Api.Infrastructures;
using Identity.Api.Infrastructures.Installed;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using VietBND.AspNetCore;
using VietBND.AspNetCore.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration
    .AddJsonFile($"appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container => container.AddVietBNDFramework());
builder.Host.ConfigureContainer<ContainerBuilder>(container => container.AddServices());

builder.Services.AddDbContext<IdentityDbContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services
    .AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddOAuth("ShoppingCart",options =>
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



//builder.Host.ConfigureWebHostDefaults((webBuilder) =>
//{
//    webBuilder.ConfigureKestrel(options =>
//    {
//        options.ListenLocalhost(int.Parse(configuration["GRPC_PORT"]), configure =>
//        {
//            configure.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
//        });
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseMiddleware<JwtMiddleware>();

app.ConfigureEventBus();

app.MapControllers();

app.Run();
