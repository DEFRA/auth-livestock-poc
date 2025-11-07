using System.Reflection;

namespace Livestock.Auth.Database;

public class AuthContext(DbContextOptions<AuthContext> options): DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasPostgresExtension(PostgreExtensions.UUIDGenerator);
        modelBuilder.HasDefaultSchema(Constants.SchemaName);
    }
}