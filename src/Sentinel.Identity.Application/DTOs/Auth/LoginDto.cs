﻿namespace Sentinel.Identity.Application.DTOs.Auth;

public record LoginDto(
    string UsernameOrEmail,
    string Password
);