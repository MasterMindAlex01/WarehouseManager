using MediatR;

namespace WarehouseManager.Api.Features.Identity.Tokens;

public class RefreshTokenRequest : IRequest<IResult>
{
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
