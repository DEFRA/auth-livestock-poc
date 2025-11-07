using System.Reflection;
using Livestock.Auth.Database.Entities;

namespace Livestock.Auth.Database;

public class AuthContext(DbContextOptions<AuthContext> options): DbContext(options)
{
    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // modelBuilder.HasDefaultSchema(Constants.SchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasPostgresExtension(PostgreExtensions.UUIDGenerator);
       
    }
}