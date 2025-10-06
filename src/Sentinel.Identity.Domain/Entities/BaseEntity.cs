namespace Sentinel.Identity.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdateAt { get; set; }
    public char Status { get; set; } = 'A'; // A=Activo, I=Inactivo
    
    public void Activate() => Status = 'A';
    public void Deactivate() => Status = 'I';
    public bool IsActive() => Status == 'A';
    
    protected void MarkAsUpdated()
    {
        UpdateAt = DateTime.UtcNow;
    }
}