using FluentValidation;

namespace WarehouseManager.Api.Features.Identity.Tokens;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(p => p.Token)
            .NotEmpty().WithMessage("El token es obligatorio.");
        RuleFor(p => p.RefreshToken)
            .NotEmpty().WithMessage("El token de actualización es obligatorio.");
    }
}
