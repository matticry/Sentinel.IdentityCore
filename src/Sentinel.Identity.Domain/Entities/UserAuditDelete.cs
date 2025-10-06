namespace Sentinel.Identity.Domain.Entities;

public class UserAuditDelete
{
    public int AuditId { get; private set; }
    public int UserId { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Dni { get; private set; }
    public string? Phone { get; private set; }
    public string? Address { get; private set; }
    public char Status { get; private set; }
    public int Age { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public DateTime DeletedAt { get; private set; }
    public string? DeletedBy { get; private set; }
}