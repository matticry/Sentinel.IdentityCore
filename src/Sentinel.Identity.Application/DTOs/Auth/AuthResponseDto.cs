namespace Sentinel.Identity.Application.DTOs.Auth;

public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    int UserId,
    string Username,
    string Email
);