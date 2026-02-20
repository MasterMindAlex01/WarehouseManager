using FluentValidation;

namespace WarehouseManager.Api.Features.Multitenancy;

public class GetTenantRequestValidator : AbstractValidator<GetTenantRequest>
{
    public GetTenantRequestValidator() =>
        RuleFor(t => t.TenantId)
            .NotEmpty();
}
