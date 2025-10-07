using MediatR;
using Sentinel.Identity.Application.DTOs.Auth;

namespace Sentinel.Identity.Application.Commands.Auth;

public record RefreshTokenCommand(RefreshTokenDto RefreshToken, string IpAddress) : IRequest<ApiResponse<AuthResponseDto>>;
