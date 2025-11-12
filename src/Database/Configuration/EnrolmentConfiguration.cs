using Livestock.Auth.Database.Configuration.Base;
using Livestock.Auth.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livestock.Auth.Database.Configuration;

internal class EnrolmentConfiguration : BaseUpdateEntityConfiguration<Enrolment>
{
    public override void Configure(EntityTypeBuilder<Enrolment> builder)
    {
        builder.HasIndex(x => new  {x.B2cObjectId, x.Role}).IsUnique();
        
        builder.Property(x => x.B2cObjectId)
            .HasColumnName(nameof(Enrolment.B2cObjectId).ToSnakeCase())
            .HasColumnType(ColumnTypes.UniqueIdentifier);
        
        builder.Property(x => x.ApplicationId)
            .HasColumnName(nameof(Enrolment.ApplicationId).ToSnakeCase())
            .HasColumnType(ColumnTypes.UniqueIdentifier);
        
        builder.Property(x => x.CphId)
            .HasColumnName(nameof(Enrolment.CphId).ToSnakeCase())
            .HasColumnType(ColumnTypes.Text);
        
        builder.Property(x => x.Role)
            .HasColumnName(nameof(Enrolment.Role).ToSnakeCase())
            .HasColumnType(ColumnTypes.Text);
        
        builder.Property(x => x.Scope)
            .HasColumnName(nameof(Enrolment.Scope).ToSnakeCase())
            .HasColumnType(ColumnTypes.JsonB);
        
        builder.Property(x => x.Status)
            .HasColumnName(nameof(Enrolment.Status).ToSnakeCase())
            .HasColumnType(ColumnTypes.Text)
            .HasDefaultValue("active");
        
        builder.Property(x => x.EnrolledAt)
            .HasColumnName(nameof(Enrolment.EnrolledAt).ToSnakeCase())
            .HasColumnType(ColumnTypes.Timestamp)
            .HasDefaultValueSql("now()");

        
        builder.Property(x => x.ExpiresAt)
            .HasColumnName(nameof(Enrolment.ExpiresAt).ToSnakeCase())
            .HasColumnType(ColumnTypes.Timestamp);
        
        
        base.Configure(builder);
    }
}