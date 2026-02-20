using FluentValidation;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("A valid email address is required.");
        RuleFor(p => p.Password)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        RuleFor(p => p.Token)
            .NotEmpty().WithMessage("Reset token is required.");
    }
}