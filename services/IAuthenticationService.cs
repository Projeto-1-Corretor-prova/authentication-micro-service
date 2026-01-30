using authentication_micro_service.dtos;
using authentication_micro_service.entities;

namespace authentication_micro_service.services;

public interface IAuthenticationService
{
    Task<UserAuthentication?> RefreshAsync(string refreshToken);
    
    Task<UserAuthentication?> RegisterAsync(RegisterDto registerDto);

    Task<UserAuthentication?> LoginAsync(LoginDto loginDto);
    
    Task<bool> RevokeTokenAsync(string refreshToken);
}