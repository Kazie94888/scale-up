using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;
using ScaleUp.Core.Domain.Entities.Tenants;

namespace ScaleUp.Core.Persistence.Configurations;

internal class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToCollection("Tenants");

        builder.HasKey(o => o.Id);
        
        builder.OwnsMany(x => x._history, onb =>
        {
            onb.ToJson();
        });
    }
}