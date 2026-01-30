using authentication_micro_service.entities;

namespace authentication_micro_service.services;

public class RefreshTokenService : IRefreshTokenService
{
    public Task SaveRefreshTokenAsync(int userId, string s, string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<User?> ValidateRefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RevokeTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }
}