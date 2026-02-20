using FluentValidation;

namespace WarehouseManager.Api.Features.Identity.Users;

public class UserRolesRequestValidator : AbstractValidator<UserRolesRequest>
{
    public UserRolesRequestValidator()
    {
        RuleFor(r => r.UserId)
            .NotEmpty().WithMessage("User ID is required.");
        RuleForEach(r => r.UserRoles).SetValidator(new UserRoleDtoValidator());
    }
}

