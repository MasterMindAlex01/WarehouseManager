using MediatR;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ResetPasswordRequest : IRequest<IResult>
{
    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Token { get; set; }
}
