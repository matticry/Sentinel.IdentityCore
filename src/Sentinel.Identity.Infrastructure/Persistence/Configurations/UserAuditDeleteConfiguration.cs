using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sentinel.Identity.Domain.Entities;

namespace Sentinel.Identity.Infrastructure.Persistence.Configurations;

public class UserAuditDeleteConfiguration : IEntityTypeConfiguration<UserAuditDelete>
{
    public void Configure(EntityTypeBuilder<UserAuditDelete> builder)
    {
        builder.ToTable("tbl_user_audit_delete", "public");

        builder.HasKey(a => a.AuditId);
        builder.Property(a => a.AuditId)
            .HasColumnName("audit_id")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.UserId)
            .HasColumnName("id_us")
            .IsRequired();

        builder.Property(a => a.Name)
            .HasColumnName("name_us")
            .HasMaxLength(100);

        builder.Property(a => a.LastName)
            .HasColumnName("lastname_us")
            .HasMaxLength(100);

        builder.Property(a => a.Dni)
            .HasColumnName("dni_us")
            .HasMaxLength(10)
            .IsFixedLength();

        builder.Property(a => a.Phone)
            .HasColumnName("phone_us")
            .HasMaxLength(20);

        builder.Property(a => a.Address)
            .HasColumnName("address_us")
            .HasMaxLength(255);

        builder.Property(a => a.Status)
            .HasColumnName("status_us")
            .HasMaxLength(1)
            .IsFixedLength();

        builder.Property(a => a.Age)
            .HasColumnName("age_us");

        builder.Property(a => a.Username)
            .HasColumnName("username_us")
            .HasMaxLength(50);

        builder.Property(a => a.Email)
            .HasColumnName("email_us")
            .HasMaxLength(255);

        builder.Property(a => a.DeletedAt)
            .HasColumnName("deleted_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(a => a.DeletedBy)
            .HasColumnName("deleted_by")
            .HasMaxLength(50);

        builder.HasIndex(a => a.UserId)
            .HasDatabaseName("ix_tbl_user_audit_delete_user");
    }
}