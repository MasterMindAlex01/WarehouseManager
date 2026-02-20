using MediatR;

namespace WarehouseManager.Api.Features.Multitenancy;
public class DeactivateTenantRequest : IRequest<IResult>
{
    public string TenantId { get; set; } = default!;

    public DeactivateTenantRequest(string tenantId) => TenantId = tenantId;
}
