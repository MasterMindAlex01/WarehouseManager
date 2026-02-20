using MediatR;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ForgotPasswordRequest : IRequest<IResult>
{
    public string Email { get; set; } = default!;
}
