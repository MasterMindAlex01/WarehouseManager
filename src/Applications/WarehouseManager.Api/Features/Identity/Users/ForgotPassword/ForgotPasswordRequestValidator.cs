using FluentValidation;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordRequestValidator() =>
        RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
                .WithMessage("Invalid Email Address.");
}