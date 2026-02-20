using MediatR;

namespace WarehouseManager.Api.Features.Identity.Tokens;

public class TokenRequest : IRequest<IResult>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
