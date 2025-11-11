using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Livestock.Auth.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthDatabase(this IServiceCollection services, IConfiguration configuration)
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
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(10),
                                errorCodesToAdd: null);
                            npgsqlOptions.CommandTimeout(60);
                        })
                    .EnableSensitiveDataLogging(isProd);
            });
      
        return services;
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