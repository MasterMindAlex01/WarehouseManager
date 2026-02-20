using MediatR;
using WarehouseManager.Application.Multitenancy;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Multitenancy;

public class ActivateTenantRequestHandler : IRequestHandler<ActivateTenantRequest, IResult>
{
    private readonly ITenantService _tenantService;

    public ActivateTenantRequestHandler(ITenantService tenantService) => _tenantService = tenantService;

    public async Task<IResult> Handle(ActivateTenantRequest request, CancellationToken cancellationToken)
    {
        var response = await _tenantService.ActivateAsync(request.TenantId);
        return Results.Ok(Result.Success(response));
    }
}