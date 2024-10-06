using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using ScaleUp.Core.Domain.Entities.Products;

namespace ScaleUp.Core.Persistence.Configurations;

internal class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.ToCollection("Vendors");

        builder.HasKey(o => o.Id);
    }
}