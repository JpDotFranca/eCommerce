namespace Customers.Domain.Entities;

abstract class Entity
{
    public int Id { get; protected set; }

    protected Entity() { }

    protected Entity(int id) => Id = id;
}

