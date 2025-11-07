using Livestock.Auth.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livestock.Auth.Database.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       builder.ToTable("Users");
       builder.HasKey(x => x.Id);
       builder.HasIndex(x => x.Id).IsUnique();
       
       builder.Property(x => x.Id)
           .HasColumnType(ColumnTypes.UniqueIdentifier);

    }
}