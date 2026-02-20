namespace WarehouseManager.Application.Identity.Tokens;

public record RefreshTokenRequestDto(string Token, string RefreshToken);