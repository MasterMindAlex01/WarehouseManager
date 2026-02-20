using MediatR;

namespace WarehouseManager.Api.Features.Multitenancy;

public class ActivateTenantRequest : IRequest<IResult>
{
    public string TenantId { get; set; } = default!;

    public ActivateTenantRequest(string tenantId) => TenantId = tenantId;
}
