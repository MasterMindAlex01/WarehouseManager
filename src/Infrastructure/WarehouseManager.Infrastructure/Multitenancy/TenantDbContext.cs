using Finbuckle.MultiTenant.Stores;
using WarehouseManager.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManager.Infrastructure.Multitenancy;

public class TenantDbContext : EFCoreStoreDbContext<WHMTenantInfo>
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WHMTenantInfo>().ToTable("Tenants", SchemaNames.MultiTenancy);
    }
}