using FluentValidation;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class UpdateRolePermissionsRequestValidator : AbstractValidator<UpdateRolePermissionsRequest>
{
    public UpdateRolePermissionsRequestValidator()
    {
        RuleFor(r => r.RoleId)
            .NotEmpty();
        RuleFor(r => r.Permissions)
            .NotNull();
    }
}