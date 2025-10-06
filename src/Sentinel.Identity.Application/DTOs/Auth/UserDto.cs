namespace Sentinel.Identity.Application.DTOs.Auth;

public record UserDto(
    int Id,
    string Name,
    string LastName,
    string Username,
    string Email,
    bool EmailVerified
);