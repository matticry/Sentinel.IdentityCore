namespace Sentinel.Identity.Application.DTOs.Auth;

public record LoginRequest(
    string Username,
    string Password
);