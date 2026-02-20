using FluentValidation;

namespace WarehouseManager.Api.Features.Identity.Users;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();

        RuleFor(p => p.FirstName)
            .NotEmpty()
            .MaximumLength(75);

        RuleFor(p => p.LastName)
            .NotEmpty()
            .MaximumLength(75);

        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress()
                .WithMessage("Invalid Email Address.");

        RuleFor(p => p.Image);
    }
}