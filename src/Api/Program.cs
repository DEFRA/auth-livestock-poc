using Livestock.Auth.Example.Endpoints;
using Livestock.Auth.Example.Services;
using Livestock.Auth.Utils;
using Livestock.Auth.Utils.Http;
using Livestock.Auth.Utils.Mongo;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using Livestock.Auth.Config;
using Livestock.Auth.Database;
using Livestock.Auth.Endpoints.Users;
using Livestock.Auth.Utils.Logging;
using Serilog;


var app = CreateWebApplication(args);
await app.RunAsync();
return;

[ExcludeFromCodeCoverage]
static WebApplication CreateWebApplication(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);
    ConfigureBuilder(builder);

    var app = builder.Build();
    return SetupApplication(app);
}

[ExcludeFromCodeCoverage]
static void ConfigureBuilder(WebApplicationBuilder builder)
{
    builder.Configuration.AddEnvironmentVariables();
    //builder.Services.AddDefaultAWSOptions(new AWSOptions { Region =   RegionEndpoint.EUWest2 });

    
    // Configure logging to use the CDP Platform standards.
    builder.Services.AddHttpContextAccessor();
    builder.Host.UseSerilog(CdpLogging.Configuration);
    
    // Default HTTP Client
    builder.Services
        .AddHttpClient("DefaultClient")
        .AddHeaderPropagation();

    // Proxy HTTP Client
    builder.Services.AddTransient<ProxyHttpMessageHandler>();
    builder.Services
        .AddHttpClient("proxy")
        .ConfigurePrimaryHttpMessageHandler<ProxyHttpMessageHandler>();
    builder.Services.AddAuthDatabase(builder.Configuration);
    // Propagate trace header.
    builder.Services.AddHeaderPropagation(options =>
    {
        var traceHeader = builder.Configuration.GetValue<string>("TraceHeader");
        if (!string.IsNullOrWhiteSpace(traceHeader))
        {
            options.Headers.Add(traceHeader);
        }
    });
    
    
    // Set up the MongoDB client. Config and credentials are injected automatically at runtime.
    builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection("Mongo"));
    builder.Services.AddSingleton<IMongoDbClientFactory, MongoDbClientFactory>();
   
    
    builder.Services.AddHealthChecks();
    builder.Services.AddValidatorsFromAssemblyContaining<Program>();
   
    
    // Set up the endpoints and their dependencies
   
    builder.Services.AddTransient<IUserDataService, UsersDataService>(service => new UsersDataService(service.GetRequiredService<AuthContext>()));
}

[ExcludeFromCodeCoverage]
static WebApplication SetupApplication(WebApplication app)
{
    app.UseHeaderPropagation();
    app.UseRouting();
    app.MapHealthChecks("/health");
    app.UseAuthDatabase();
    app.UseUsersEndpoints();

    return app;
}
