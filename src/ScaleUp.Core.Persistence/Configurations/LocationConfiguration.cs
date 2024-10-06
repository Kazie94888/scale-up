using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using ScaleUp.Core.Domain.Entities.Locations;
using ScaleUp.Core.Domain.Entities.Products;

namespace ScaleUp.Core.Persistence.Configurations;

internal class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToCollection("Locations");

        builder.HasKey(o => o.Id);
    }
}