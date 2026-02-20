using FluentValidation;

namespace WarehouseManager.Api.Features.Multitenancy;

public class ActivateTenantRequestValidator : AbstractValidator<ActivateTenantRequest>
{
    public ActivateTenantRequestValidator() =>
        RuleFor(t => t.TenantId)
            .NotEmpty();
}
