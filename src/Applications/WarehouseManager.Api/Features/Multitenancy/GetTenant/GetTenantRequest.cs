using MediatR;

namespace WarehouseManager.Api.Features.Multitenancy;

public class GetTenantRequest : IRequest<IResult>
{
    public string TenantId { get; set; } = default!;

    public GetTenantRequest(string tenantId) => TenantId = tenantId;
}
