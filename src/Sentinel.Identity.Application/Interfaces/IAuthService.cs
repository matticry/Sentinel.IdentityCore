using Sentinel.Identity.Application.DTOs.Auth;

namespace Sentinel.Identity.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    
}