using MediatR;
using Microsoft.Extensions.Configuration;
using Sentinel.Identity.Application.DTOs.Auth;
using Sentinel.Identity.Domain.Entities;
using Sentinel.Identity.Domain.Exceptions;
using Sentinel.Identity.Domain.Repositories;
using Sentinel.Identity.Domain.Services;

namespace Sentinel.Identity.Application.Commands.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<AuthResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<ApiResponse<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Login.UsernameOrEmail, cancellationToken)
                   ?? await _userRepository.GetByUsernameAsync(request.Login.UsernameOrEmail, cancellationToken);

        if (user == null || !_passwordHasher.Verify(request.Login.Password, user.PasswordHash))
            throw new ValidationException("Invalid credentials");

        if (!user.IsActive())
            throw new ValidationException("User account is inactive");

        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Username, user.Email);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenEntity = RefreshToken.Create(
            user.Id,
            refreshToken,
            DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpirationDays"])),
            request.IpAddress
        );

        await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

        var response = new AuthResponseDto(accessToken, refreshToken, user.Id, user.Username, user.Email);

        return ApiResponse<AuthResponseDto>.SuccessResponse(response, "Login successful");
    }
}