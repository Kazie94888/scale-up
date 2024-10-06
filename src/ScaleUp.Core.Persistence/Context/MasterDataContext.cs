using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.EntityFrameworkCore.Metadata.Conventions;
using ScaleUp.Core.Domain.Base.Interfaces;
using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Customers;
using ScaleUp.Core.Domain.Entities.Locations;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Entities.Products;
using ScaleUp.Core.Domain.Entities.Roles;
using ScaleUp.Core.Domain.Entities.Tenants;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.Domain.Entities.Warehouses;
using ScaleUp.Core.Persistence.DomainEvents;
using ScaleUp.Core.Persistence.Extensions;
using ScaleUp.Core.SharedKernel.Constants;

namespace ScaleUp.Core.Persistence.Context;

public sealed class MasterDataContext : DbContext
{
    private readonly IDomainEventsDispatcher? _domainEventsDispatcher;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MasterDataContext(DbContextOptions<MasterDataContext> options, IHttpContextAccessor httpContextAccessor, IDomainEventsDispatcher? domainEventsDispatcher = null) :
        base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(MasterDataContext).Assembly);
        // TODO: Enable this after implementing authen & autho
        //builder.ApplyGlobalQueryFilters(GetTenantId());
        builder.ApplyBaseEntityProperties();

        base.OnModelCreating(builder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(serviceProvider =>
            new CamelCaseElementNameConvention(serviceProvider
                .GetRequiredService<ProviderConventionSetBuilderDependencies>()));
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return SaveChangesAsync(true, cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        // TODO: Enable this after implementing authen & autho
        // AddTenantIdToNewEntities();

        var savedCount = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        await DispatchDomainEventsAsync(cancellationToken);

        return savedCount;
    }

    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        if (_domainEventsDispatcher is null)
            throw new ArgumentNullException(nameof(_domainEventsDispatcher), "Domain events dispatcher is null");

        await _domainEventsDispatcher.DispatchDomainEventsAsync(this, cancellationToken);
    }

    private void AddTenantIdToNewEntities()
    {
        var addedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added)
            .Select(e => e.Entity)
            .OfType<IMultiTenant>();

        var tenantId = GetTenantId();

        foreach (var entity in addedEntities)
        {
            if (entity.TenantId == Guid.Empty)
            {
                entity.TenantId = tenantId;
            }
        }
    }

    public override int SaveChanges() => throw new NotImplementedException();
    public override int SaveChanges(bool acceptAllChangesOnSuccess) => throw new NotImplementedException();

    private Guid GetTenantId()
    {
        var tenantId = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypeConstants.TenantId)?.Value ?? "C415CB0F-2DEA-464E-B64B-27D6EB40A0B4";
        

        return Guid.Parse(tenantId!);
    }
}