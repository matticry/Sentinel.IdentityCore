using MediatR;
using Sentinel.Identity.Application.DTOs.Auth;

namespace Sentinel.Identity.Application.Commands.Auth;

public record LoginCommand(LoginDto Login, string IpAddress) : IRequest<ApiResponse<AuthResponseDto>>;
