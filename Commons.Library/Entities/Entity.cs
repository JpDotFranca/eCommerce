namespace Customers.Domain.Entities;

public abstract class Entity
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    protected Entity()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    protected Entity(int id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    protected Entity(int id, DateTime createAt, DateTime updateAt)
    {
        Id = id;
        CreatedAt = createAt;
        UpdatedAt = updateAt;
    }
}
