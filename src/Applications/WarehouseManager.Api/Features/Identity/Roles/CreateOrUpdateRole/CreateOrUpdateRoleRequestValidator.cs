using FluentValidation;
using WarehouseManager.Application.Identity.Roles;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class CreateOrUpdateRoleRequestValidator : AbstractValidator<CreateOrUpdateRoleRequest>
{
    public CreateOrUpdateRoleRequestValidator() =>
        RuleFor(r => r.Name)
            .NotEmpty();
}