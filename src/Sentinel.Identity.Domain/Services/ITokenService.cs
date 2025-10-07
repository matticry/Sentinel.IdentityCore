namespace Sentinel.Identity.Domain.Services;

public interface ITokenService
{
    string GenerateAccessToken(int userId, string username, string email);
    string GenerateRefreshToken();
    int? ValidateToken(string token);
}