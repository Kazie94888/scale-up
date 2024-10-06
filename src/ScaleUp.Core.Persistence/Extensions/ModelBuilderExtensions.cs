using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Base.Interfaces;
using ScaleUp.Core.SharedKernel.Entities;
using System.Reflection;

namespace ScaleUp.Core.Persistence.Extensions;

internal static class ModelBuilderExtensions
{
    internal static void ApplyBaseEntityProperties(this ModelBuilder builder)
    {
        // add CreatedAt property to all entities
        var mutableEntityTypes = builder.Model.GetEntityTypes()
            .Where(x => typeof(Entity).IsAssignableFrom(x.ClrType))
            .ToList();
        foreach (var entity in mutableEntityTypes)
        {
            builder.Entity(entity.ClrType).Property<DateTime>(nameof(Entity.CreatedAt));
        }
    }

    internal static void ApplyGlobalQueryFilters(this ModelBuilder builder, Guid tenantId)
    {
        var setGlobalQueryMethod = typeof(ModelBuilderExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == nameof(SetGlobalQuery));

        var mutableEntityTypes = builder.Model.GetEntityTypes()
            .Where(x => typeof(IMultiTenant).IsAssignableFrom(x.ClrType)).ToList();

        foreach (var entity in mutableEntityTypes)
        {
            var method = setGlobalQueryMethod.MakeGenericMethod(entity.ClrType);
            method.Invoke(null, [builder, tenantId]);
        }
    }

    private static void SetGlobalQuery<T>(ModelBuilder builder, Guid tenantId) where T : class, IMultiTenant
    {
        builder.Entity<T>().HasQueryFilter(e => e.TenantId == tenantId);
    }
}
