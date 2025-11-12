using Livestock.Auth.Database.Configuration.Base;
using Livestock.Auth.Database.Entities;

namespace Livestock.Auth.Database.Configuration;

internal class FederationConfiguration : BaseUpdateEntityConfiguration<Federation>
{
    public override void Configure(EntityTypeBuilder<Federation> builder)
    {
        
        
        builder.Property(x => x.UserAccountId)
            .HasColumnName(nameof(Federation.UserAccountId).ToSnakeCase())
            .HasColumnType(ColumnTypes.UniqueIdentifier);
        
        builder.Property(x => x.B2cTenant)
            .HasColumnName(nameof(Federation.B2cTenant).ToSnakeCase())
            .HasColumnType(ColumnTypes.Text)
            .IsRequired();
        
        builder.Property(x => x.B2cObjectId)
            .HasColumnName(nameof(Federation.B2cObjectId).ToSnakeCase())
            .HasColumnType(ColumnTypes.UniqueIdentifier)
            
            .IsRequired();
        
        builder.Property(x => x.TrustLevel)
            .HasColumnName(nameof(Federation.TrustLevel).ToSnakeCase())
            .HasColumnType(ColumnTypes.Text)
            .HasDefaultValue("standard")
            .IsRequired();
        
        builder.Property(x => x.SyncStatus)
            .HasColumnName(nameof(Federation.SyncStatus).ToSnakeCase())
            .HasColumnType(ColumnTypes.Text)
            .HasDefaultValue("linked")
            .IsRequired();
        
        builder.Property(x => x.LastSyncedAt)
            .HasColumnName(nameof(Federation.LastSyncedAt).ToSnakeCase())
            .HasColumnType(ColumnTypes.Timestamp);
        
        
        builder.HasOne(u => u.UserAccount)
            .WithMany(o => o.Federations)
            .HasForeignKey(u => u.UserAccountId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasIndex(x => new{x.B2cObjectId, x.B2cTenant});
        
        base.Configure(builder);
        
    }
}