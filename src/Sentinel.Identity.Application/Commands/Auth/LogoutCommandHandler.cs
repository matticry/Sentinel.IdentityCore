using MediatR;
using Sentinel.Identity.Domain.Repositories;

namespace Sentinel.Identity.Application.Commands.Auth;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResponse<object>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<ApiResponse<object>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _refreshTokenRepository.RevokeAllByUserIdAsync(request.UserId, request.IpAddress, cancellationToken);

        return ApiResponse<object>.SuccessResponse("Logout successful");
    }
}