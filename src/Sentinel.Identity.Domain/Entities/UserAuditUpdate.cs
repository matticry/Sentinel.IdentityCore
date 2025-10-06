namespace Sentinel.Identity.Domain.Entities;

public class UserAuditUpdate
{
    public int AuditId { get; private set; }
    public int UserId { get; private set; }
    public string FieldName { get; private set; }
    public string? OldValue { get; private set; }
    public string? NewValue { get; private set; }
    public DateTime ChangedAt { get; private set; }
    public string? ChangedBy { get; private set; }
}