namespace Sentinel.Identity.Application.DTOs.Auth;

public record UserWriteDto(
    string Name,
    string LastName,
    string Dni,
    string? Phone,
    string? Address,
    int Age,
    string Username,
    string Email,
    string Password
);