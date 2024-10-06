using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using ScaleUp.Core.Domain.Entities.Warehouses;

namespace ScaleUp.Core.Persistence.Configurations;

internal class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToCollection("Warehouses");

        builder.HasKey(o => o.Id);
    }
}