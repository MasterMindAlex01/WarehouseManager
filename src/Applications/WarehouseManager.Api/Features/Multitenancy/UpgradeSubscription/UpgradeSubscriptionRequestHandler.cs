using MediatR;
using WarehouseManager.Application.Multitenancy;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Multitenancy;

public class UpgradeSubscriptionRequestHandler : IRequestHandler<UpgradeSubscriptionRequest, IResult>
{
    private readonly ITenantService _tenantService;

    public UpgradeSubscriptionRequestHandler(ITenantService tenantService) => _tenantService = tenantService;

    public async Task<IResult> Handle(UpgradeSubscriptionRequest request, CancellationToken cancellationToken)
    {
        var response = await _tenantService.UpdateSubscription(request.TenantId, request.ExtendedExpiryDate);
        return Results.Ok(Result.Success(response));
    }
}