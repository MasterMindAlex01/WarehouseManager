using FluentValidation;

namespace WarehouseManager.Api.Features.Multitenancy;

public class DeactivateTenantRequestValidator : AbstractValidator<DeactivateTenantRequest>
{
    public DeactivateTenantRequestValidator() =>
        RuleFor(t => t.TenantId)
            .NotEmpty();
}
