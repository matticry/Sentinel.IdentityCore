namespace Sentinel.Identity.Application.DTOs.Auth;

public record UserListDto(
    int Id,
    string Name,
    string LastName,
    string Dni,
    string Username,
    string Phone,
    string Address,
    string Email,
    bool EmailVerified,
    int Age,
    char Status,
    DateTime CreatedAt
);