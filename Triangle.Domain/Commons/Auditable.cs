namespace Triangle.Domain.Commons;

public abstract class Auditable
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastUpdatedAt { get; set;}
    public bool IsDeleted { get; set; }
    public int? UpdatedBy { get; set; }
    public int? Deletedby { get; set; }
}
