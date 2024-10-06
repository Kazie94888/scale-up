using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.AuditLogs;
using ScaleUp.Core.Domain.Entities.Customers;
using ScaleUp.Core.Domain.Entities.Locations;
using ScaleUp.Core.Domain.Entities.Orders;
using ScaleUp.Core.Domain.Entities.Products;
using ScaleUp.Core.Domain.Entities.Roles;
using ScaleUp.Core.Domain.Entities.Tenants;
using ScaleUp.Core.Domain.Entities.Users;
using ScaleUp.Core.Domain.Entities.Warehouses;
using Warehouse = ScaleUp.Core.Domain.Entities.Warehouses.Warehouse;

namespace ScaleUp.Core.Persistence.Context;

public sealed class ReadOnlyMasterDataContext
{
    private readonly MasterDataContext _context;

    public ReadOnlyMasterDataContext(MasterDataContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public IQueryable<Order> Orders => _context.Orders.IgnoreAutoIncludes();
    public IQueryable<AuditLog> AuditLogs => _context.AuditLogs.IgnoreAutoIncludes();
    public IQueryable<User> Users => _context.Users.IgnoreAutoIncludes();
    public IQueryable<Product> Products => _context.Products.IgnoreAutoIncludes();
    public IQueryable<Location> Locations => _context.Locations.IgnoreAutoIncludes();
    public IQueryable<Customer> Customers => _context.Customers.IgnoreAutoIncludes();
    public IQueryable<Warehouse> Warehouses => _context.Warehouses.IgnoreAutoIncludes();
    public IQueryable<Tenant> Tenants => _context.Tenants.IgnoreAutoIncludes();
    public IQueryable<Role> Roles => _context.Roles.IgnoreAutoIncludes();
}