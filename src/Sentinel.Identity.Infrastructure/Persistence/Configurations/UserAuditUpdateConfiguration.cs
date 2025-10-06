using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sentinel.Identity.Domain.Entities;

namespace Sentinel.Identity.Infrastructure.Persistence.Configurations;

public class UserAuditUpdateConfiguration : IEntityTypeConfiguration<UserAuditUpdate>
{
    public void Configure(EntityTypeBuilder<UserAuditUpdate> builder)
    {
        builder.ToTable("tbl_user_audit_update", "public");

        builder.HasKey(a => a.AuditId);
        builder.Property(a => a.AuditId)
            .HasColumnName("audit_id")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.UserId)
            .HasColumnName("id_us")
            .IsRequired();

        builder.Property(a => a.FieldName)
            .HasColumnName("field_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.OldValue)
            .HasColumnName("old_value")
            .HasColumnType("TEXT");

        builder.Property(a => a.NewValue)
            .HasColumnName("new_value")
            .HasColumnType("TEXT");

        builder.Property(a => a.ChangedAt)
            .HasColumnName("changed_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(a => a.ChangedBy)
            .HasColumnName("changed_by")
            .HasMaxLength(50);

        builder.HasIndex(a => a.UserId)
            .HasDatabaseName("ix_tbl_user_audit_update_user");
    }
}