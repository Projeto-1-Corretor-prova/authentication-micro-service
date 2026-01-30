using authentication_micro_service.entities;

namespace authentication_micro_service.services;

public interface IRefreshTokenService
{
    Task SaveRefreshTokenAsync(int userId, string refreshToken, string acessToken);
    Task<User?> ValidateRefreshTokenAsync(string refreshToken);
    Task<bool> RevokeTokenAsync(string refreshToken);
}