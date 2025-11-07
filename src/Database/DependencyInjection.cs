using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Livestock.Auth.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthContext>(options => options.UseNpgsql(configuration.GetConnectionString("Auth")));
        return services;
    }
}