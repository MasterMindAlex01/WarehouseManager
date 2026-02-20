using FluentValidation;

namespace WarehouseManager.Api.Features.Multitenancy.CreateTenant;

public class CreateTenantRequestValidator : AbstractValidator<CreateTenantRequest>
{
    public CreateTenantRequestValidator()
    {
        RuleFor(t => t.Id).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(t => t.Identifier).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(t => t.Name).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(t => t.ConnectionString)
            .NotEmpty();

        RuleFor(t => t.AdminEmail).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}