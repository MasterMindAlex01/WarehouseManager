using MediatR;
using WarehouseManager.Application.Multitenancy;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Multitenancy;

public class DeactivateTenantRequestHandler : IRequestHandler<DeactivateTenantRequest, IResult>
{
    private readonly ITenantService _tenantService;

    public DeactivateTenantRequestHandler(ITenantService tenantService) => _tenantService = tenantService;

    public async Task<IResult> Handle(DeactivateTenantRequest request, CancellationToken cancellationToken)
    {
        var response = await _tenantService.DeactivateAsync(request.TenantId);
        return Results.Ok(Result.Success(response));
    }
}