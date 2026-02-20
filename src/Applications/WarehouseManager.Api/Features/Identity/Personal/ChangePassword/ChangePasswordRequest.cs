using MediatR;

namespace WarehouseManager.Api.Features.Identity.Personal;

public class ChangePasswordRequest : IRequest<IResult>
{
    public string Password { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
    public string ConfirmNewPassword { get; set; } = default!;
}
