namespace Sentinel.Identity.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public int UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    public string? RevokedByIp { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public string CreatedByIp { get; private set; }

    private RefreshToken() { }

    public static RefreshToken Create(int userId, string token, DateTime expiresAt, string createdByIp)
    {
        return new RefreshToken
        {
            UserId = userId,
            Token = token,
            ExpiresAt = expiresAt,
            IsRevoked = false,
            CreatedByIp = createdByIp
        };
    }

    public void Revoke(string revokedByIp)
    {
        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
        RevokedByIp = revokedByIp;
    }

    public bool IsActive() => !IsRevoked && ExpiresAt > DateTime.UtcNow;
}