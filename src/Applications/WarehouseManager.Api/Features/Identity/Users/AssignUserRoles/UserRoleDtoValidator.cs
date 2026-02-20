using FluentValidation;
using WarehouseManager.Application.Identity.Users;

namespace WarehouseManager.Api.Features.Identity.Users;

public class UserRoleDtoValidator : AbstractValidator<UserRoleDto>
{
    public UserRoleDtoValidator()
    {
        RuleFor(r => r.RoleId)
            .NotEmpty().WithMessage("Role ID is required.");
        RuleFor(r => r.RoleName)
            .NotEmpty().WithMessage("Role Name is required.");
    }
}

