using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Livestock.Auth.Database;

public static class ServiceCollectionExtensions
{
    private  const int MaxRetryCount = 5;
    private const int MaxRetryDelay = 10;
    private const int CommandTimeout = 60;
    public static void AddAuthDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(Constants.ConnectionStringName);
        services
            .AddPooledDbContextFactory<AuthContext>((sp, options) =>
            {
                var env = sp.GetRequiredService<IHostEnvironment>();
                var isProd = env.IsProduction();
                options
                    .UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>())
                    .UseNpgsql(
                        connectionString,
                        npgsqlOptions =>
                        {
                            npgsqlOptions.EnableRetryOnFailure(
                                maxRetryCount: MaxRetryCount,
                                maxRetryDelay: TimeSpan.FromSeconds(MaxRetryDelay),
                                errorCodesToAdd: null);
                            npgsqlOptions.CommandTimeout(CommandTimeout);
                        })
                    .EnableSensitiveDataLogging(isProd);
            });
    }
    
    public static void UseAuthDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AuthContext>>();
        using var context = factory.CreateDbContext();
        
        if(context.Database.CanConnect()) 
            context.Database.OpenConnection();
    }
}