using MediatR;
using Microsoft.Extensions.Configuration;
using Sentinel.Identity.Application.DTOs.Auth;
using Sentinel.Identity.Domain.Entities;
using Sentinel.Identity.Domain.Exceptions;
using Sentinel.Identity.Domain.Repositories;
using Sentinel.Identity.Domain.Services;

namespace Sentinel.Identity.Application.Commands.Auth;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<AuthResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public RefreshTokenCommandHandler(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITokenService tokenService,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<ApiResponse<AuthResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken.RefreshToken, cancellationToken);

        if (refreshToken == null || !refreshToken.IsActive())
            throw new ValidationException("Invalid or expired refresh token");

        var user = await _userRepository.GetByIdAsync(refreshToken.UserId, cancellationToken)
            ?? throw new NotFoundException("User not found");

        if (!user.IsActive())
            throw new ValidationException("User account is inactive");

        refreshToken.Revoke(request.IpAddress);
        await _refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);

        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Username, user.Email);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        var newRefreshTokenEntity = RefreshToken.Create(
            user.Id,
            newRefreshToken,
            DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenExpirationDays"])),
            request.IpAddress
        );

        await _refreshTokenRepository.AddAsync(newRefreshTokenEntity, cancellationToken);

        var response = new AuthResponseDto(accessToken, newRefreshToken, user.Id, user.Username, user.Email);

        return ApiResponse<AuthResponseDto>.SuccessResponse(response, "Token refreshed successfully");
    }
}