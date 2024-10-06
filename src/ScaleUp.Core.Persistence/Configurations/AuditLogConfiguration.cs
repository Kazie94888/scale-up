using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using ScaleUp.Core.Domain.Entities.AuditLogs;

namespace ScaleUp.Core.Persistence.Configurations;

internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToCollection("AuditLogs");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Event).HasMaxLength(100);
    }
}