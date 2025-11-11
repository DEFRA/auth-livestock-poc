using Livestock.Auth.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livestock.Auth.Database.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       builder.ToTable("users");
       builder.HasKey(x => x.Id);
       builder.HasIndex(x => x.Id).IsUnique();
       
       builder.Property(x => x.Id)
           .HasColumnName(nameof(User.Email).ToSnakeCase())
           .HasColumnType(ColumnTypes.UniqueIdentifier);

       builder.Property(x => x.Email)
           .HasColumnName("email_address")
           .HasColumnType(ColumnTypes.Varchar)
           .HasMaxLength(256);
       
       builder.Property(x => x.Created)
           .HasColumnName("created_datetime")
           .HasColumnType(ColumnTypes.Timestamp);
       
       builder.Property(x => x.Deleted)
           .HasColumnName("deleted_datetime")
           .HasColumnType(ColumnTypes.Timestamp);
       
       builder.Property(x => x.IsActive)
           .HasColumnName("is_active")
           .HasColumnType(ColumnTypes.Boolean);
       
       

    }
}