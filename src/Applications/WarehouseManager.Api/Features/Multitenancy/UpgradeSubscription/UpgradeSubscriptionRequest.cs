using MediatR;

namespace WarehouseManager.Api.Features.Multitenancy;

public class UpgradeSubscriptionRequest : IRequest<IResult>
{
    public string TenantId { get; set; } = default!;
    public DateTime ExtendedExpiryDate { get; set; }
}
