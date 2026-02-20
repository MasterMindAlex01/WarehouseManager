using FluentValidation;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ToggleUserStatusRequestValidator : AbstractValidator<ToggleUserStatusRequest>
{
    public ToggleUserStatusRequestValidator()
    {
        RuleFor(r => r.UserId)
            .NotEmpty()
            .NotNull()
            .WithMessage("UserId is required");
        
        RuleFor(r => r.ActivateUser)
            .NotNull()
            .WithMessage("ActivateUser cannot be null");
    }
}
