using MediatR;
using WarehouseManager.Api.Helpers;
using WarehouseManager.Application.Identity.Tokens;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Tokens;

public class TokenRequestHandler : IRequestHandler<TokenRequest, IResult>
{
    private readonly ITokenService _tokenService;
    private readonly IHttpExtensionHelper _httpExtensionHelper;

    public TokenRequestHandler(
        ITokenService tokenService,
        IHttpExtensionHelper httpExtensionHelper)
    {
        _tokenService = tokenService;
        _httpExtensionHelper = httpExtensionHelper;
    }

    public async Task<IResult> Handle(TokenRequest request, CancellationToken cancellationToken)
    {
        var tokenRequestDto = new TokenRequestDto(request.Email, request.Password);
        TokenResponse token = await _tokenService
            .GetTokenAsync(tokenRequestDto, _httpExtensionHelper.GetIpAddress()!, cancellationToken);
        return Results.Ok(Result<TokenResponse>.Success(token, "Ok"));
    }
}
