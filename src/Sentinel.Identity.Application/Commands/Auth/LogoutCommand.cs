using MediatR;

namespace Sentinel.Identity.Application.Commands.Auth;

public record LogoutCommand(int UserId, string IpAddress) : IRequest<ApiResponse<object>>;
