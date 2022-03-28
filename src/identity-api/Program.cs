using identity_api.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using VietBND.AspNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Configuration
    .AddConfigurationByEnvironment();
builder.Host.UseSerilog((context, lc) =>
{
    lc.WriteTo.Console()
      .WriteTo.File($"logs\\log_{DateTime.UtcNow.ToString("d")}.text").MinimumLevel.Information()
      .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(context.Configuration["Elasticsearch:Uri"]))
      {

          AutoRegisterTemplate = true,
          IndexFormat = $"log-system",
          BufferCleanPayload = (failingEvent, statuscode, exception) =>
          {
              Console.WriteLine(failingEvent);
              dynamic e = JObject.Parse(failingEvent);
              return JsonConvert.SerializeObject(new Dictionary<string, object>()
                        {
                            { "@timestamp",e["@timestamp"]},
                            { "traceId",e.traceId },
                            { "level",e.Level},
                            { "message","Error: "+e.message},
                            { "failingException", exception}
                        });
          }
      }).MinimumLevel.Information();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
