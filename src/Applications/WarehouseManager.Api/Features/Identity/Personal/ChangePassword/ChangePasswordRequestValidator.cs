using FluentValidation;
using Microsoft.Extensions.Localization;

namespace WarehouseManager.Api.Features.Identity.Personal;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator(IStringLocalizer<ChangePasswordRequestValidator> T)
    {
        RuleFor(p => p.Password)
            .NotEmpty();

        RuleFor(p => p.NewPassword)
            .NotEmpty();

        RuleFor(p => p.ConfirmNewPassword)
            .Equal(p => p.NewPassword)
                .WithMessage(T["Passwords do not match."]);
    }
}
