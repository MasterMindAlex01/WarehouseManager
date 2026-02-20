using FluentValidation;

namespace WarehouseManager.Api.Features.Multitenancy;

public class UpgradeSubscriptionRequestValidator : AbstractValidator<UpgradeSubscriptionRequest>
{
    public UpgradeSubscriptionRequestValidator()
    {
        RuleFor(t => t.TenantId)
            .NotEmpty();
        RuleFor(t => t.ExtendedExpiryDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Extended expiry date must be in the future.");
    }
}
