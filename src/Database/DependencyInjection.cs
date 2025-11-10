using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Livestock.Auth.Database;

public static class DependencyInjection
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
        services.AddDbContext<AuthContext>(options => options.UseNpgsql(connectionString: connectionString));
        return services;
    }
}