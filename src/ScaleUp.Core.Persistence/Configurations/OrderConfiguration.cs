using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;
using ScaleUp.Core.Domain.Entities.Orders;

namespace ScaleUp.Core.Persistence.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToCollection("Orders");

        builder.HasKey(o => o.Id);
        builder.OwnsMany(x => x._history, onb =>
        {
            onb.ToJson();
        });
    }
}