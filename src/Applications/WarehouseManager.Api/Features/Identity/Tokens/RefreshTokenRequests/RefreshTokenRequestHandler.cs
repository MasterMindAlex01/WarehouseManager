using MediatR;
using WarehouseManager.Api.Helpers;
using WarehouseManager.Application.Identity.Tokens;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Tokens;

public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, IResult>
{
    private readonly ITokenService _tokenService;
    private readonly IHttpExtensionHelper _httpExtensionHelper;
    
    public RefreshTokenRequestHandler(
        ITokenService tokenService,
        IHttpExtensionHelper httpExtensionHelper)
    {
        _tokenService = tokenService;
        _httpExtensionHelper = httpExtensionHelper;
    }

    public async Task<IResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var ipAddress = _httpExtensionHelper.GetIpAddress();
        var refreshTokenDto = new RefreshTokenRequestDto(request.Token, request.RefreshToken);
        TokenResponse token = await _tokenService.RefreshTokenAsync(refreshTokenDto, ipAddress!);
        return Results.Ok(Result<TokenResponse>.Success(token, "Ok"));
    }
}