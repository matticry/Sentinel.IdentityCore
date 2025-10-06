﻿namespace Sentinel.Identity.Application.DTOs.Auth;

public record LoginResponse(
    string Token,
    string RefreshToken,
    DateTime ExpiresAt,
    UserDto User
);