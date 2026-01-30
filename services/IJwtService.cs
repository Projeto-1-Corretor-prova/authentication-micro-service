using System.Security.Claims;
using authentication_micro_service.entities;

namespace authentication_micro_service.services;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateToken(string token, bool validateLifetime = true);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);

}