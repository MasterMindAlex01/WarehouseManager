namespace WarehouseManager.Application.Identity.Tokens;

public interface ITokenService : ITransientService
{
    Task<TokenResponse> GetTokenAsync(TokenRequestDto request, string ipAddress, CancellationToken cancellationToken);

    Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequestDto request, string ipAddress);
}