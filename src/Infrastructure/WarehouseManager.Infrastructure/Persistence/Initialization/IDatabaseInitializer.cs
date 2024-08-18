using WarehouseManager.Infrastructure.Multitenancy;

namespace WarehouseManager.Infrastructure.Persistence.Initialization;

internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);
    Task InitializeApplicationDbForTenantAsync(WHMTenantInfo tenant, CancellationToken cancellationToken);
}