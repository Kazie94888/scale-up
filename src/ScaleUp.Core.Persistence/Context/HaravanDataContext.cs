using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.EntityFrameworkCore.Metadata.Conventions;
using ScaleUp.Integrations.Haravan.Base.Entities;
using ScaleUp.Integrations.Haravan.Orders;

namespace ScaleUp.Core.Persistence.Context;

public sealed class HaravanDataContext(DbContextOptions<HaravanDataContext> options) : DbContext(options)
{
    public DbSet<HaravanSyncedOrder> HaravanOrders => Set<HaravanSyncedOrder>();
    public DbSet<HaravanSyncSetting> HaravanSyncSettings => Set<HaravanSyncSetting>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(HaravanDataContext).Assembly);

        base.OnModelCreating(builder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(serviceProvider =>
            new CamelCaseElementNameConvention(serviceProvider
                .GetRequiredService<ProviderConventionSetBuilderDependencies>()));
    }
}
