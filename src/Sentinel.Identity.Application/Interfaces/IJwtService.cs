using Sentinel.Identity.Domain.Entities;

namespace Sentinel.Identity.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    bool ValidateToken(string token);
}