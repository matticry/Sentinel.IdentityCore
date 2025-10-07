using Sentinel.Identity.Application.DTOs.Auth;

namespace Sentinel.Identity.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
    
}