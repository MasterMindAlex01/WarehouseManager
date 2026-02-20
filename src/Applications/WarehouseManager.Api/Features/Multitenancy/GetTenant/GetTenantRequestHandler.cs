using MediatR;
using WarehouseManager.Application.Multitenancy;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Multitenancy;

public class GetTenantRequestHandler : IRequestHandler<GetTenantRequest, IResult>
{
    private readonly ITenantService _tenantService;

    public GetTenantRequestHandler(ITenantService tenantService) => _tenantService = tenantService;

    public async Task<IResult> Handle(GetTenantRequest request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantService.GetByIdAsync(request.TenantId);
        return Results.Ok(Result<TenantDto>.Success(tenant));
    }
}