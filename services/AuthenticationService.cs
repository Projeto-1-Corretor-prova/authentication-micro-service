using System.Net;
using authentication_micro_service.dtos;
using authentication_micro_service.entities;
using authentication_micro_service.repositories;

namespace authentication_micro_service.services;

using static BCrypt.Net.BCrypt;

public class AuthenticationService(
    IUserRepository userRepository,
    IJwtService jwtService,
    IRefreshTokenService refreshTokenService,
    ILogger<AuthenticationService> logger
    ) : IAuthenticationService
{

    public async Task<UserAuthentication?> RefreshAsync(string refreshToken)
    {
        User? userExpired = await refreshTokenService.ValidateRefreshTokenAsync(refreshToken);

        if (userExpired == null) return null;

        return await GenerateTokens(userExpired);
    }

    public async Task<UserAuthentication?> RegisterAsync(RegisterDto registerDto)
    {
        var newUser = new User()
        {
            Email = registerDto.Email,
            Username = registerDto.Username,
            Password = registerDto.Password
        };

        try
        {
            var user = await userRepository.CreateUser(newUser);
            return await LoginAsync(new LoginDto(user.Username, user.Password, user.Email));
        }
        catch
        {
            return null;
        }
    }

    public async Task<UserAuthentication?> LoginAsync(LoginDto loginDto)
    {
        var user = await ValidateCredentialsAsync(loginDto);
        
        if (user == null)
        {
            logger.LogWarning($"Authentication failed for username: {loginDto.Username} and email: {loginDto.Email}");
            return null;
        }

        return await GenerateTokens(user);
    }

    public async Task<bool> RevokeTokenAsync(string refreshToken)
    {
        bool isRevoked = await refreshTokenService.RevokeTokenAsync(refreshToken);
        return isRevoked;
    }

    private async Task<UserAuthentication> GenerateTokens(User user)
    {
        var accessToken = jwtService.GenerateAccessToken(user);
        var refreshToken = jwtService.GenerateRefreshToken();
        
        await refreshTokenService.SaveRefreshTokenAsync(user.Id, refreshToken, accessToken);

        logger.LogInformation($"User {user.Username} authenticated successfully");
        
        return new UserAuthentication(
            new MiniUserDto(user.Id, user.Username), 
            accessToken, 
            refreshToken,
            DateTime.UtcNow.AddMinutes(15)
        );
    }
    
    private async Task<User?> ValidateCredentialsAsync(LoginDto loginDto)
    {
        User? user;
        
        if (loginDto.Email != null && loginDto.Email.Any())
            user = await userRepository.GetUserByEmail(loginDto.Email);
        else if (loginDto.Username != null && loginDto.Username.Any())
            user = await userRepository.GetUserByName(loginDto.Username);
        else
            return null;
        
        if (!Verify(loginDto.Password, user!.Password))
        {
            return null;
        }
        
        return user;
    }
    
}