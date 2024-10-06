namespace ScaleUp.Core.SharedKernel.Entities;

public abstract class Entity
{
    protected Entity()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; set; }
    public required UserInfo CreatedBy { get; set; }
}