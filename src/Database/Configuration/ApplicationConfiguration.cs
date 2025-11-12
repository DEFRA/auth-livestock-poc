using Livestock.Auth.Database.Configuration.Base;
using Livestock.Auth.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livestock.Auth.Database.Configuration;

internal class ApplicationConfiguration :BaseUpdateEntityConfiguration<Application>
{
    public override void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.Property(x => x.Name)
            .HasColumnName(nameof(Application.Name).ToSnakeCase())
            .HasColumnType(ColumnTypes.Text);
        
        builder.Property(x => x.B2cClientId)
            .HasColumnName(nameof(Application.B2cClientId).ToSnakeCase())
            .HasColumnType(ColumnTypes.UniqueIdentifier);
        
        builder.Property(x => x. B2cTenant)
            .HasColumnName(nameof(Application.B2cTenant).ToSnakeCase())
            .HasColumnType(ColumnTypes.Text);
        
        builder.Property(x => x.Description)
            .HasColumnName(nameof(Application.Description).ToSnakeCase())
            .HasColumnType(ColumnTypes.Text);
        
        builder.Property(x => x.Status)
            .HasColumnName(nameof(Application.Status).ToSnakeCase())
            .HasColumnType(ColumnTypes.Text)
            .HasDefaultValue("active");
        
        base.Configure(builder);
    }
}