namespace authentication_micro_service.dtos;

public record UserAuthentication(
    MiniUserDto User,
    string AcessToken,
    string RefreshToken,
    DateTime ExpiresAt
    );