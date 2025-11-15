using Commons.Library.Entities.ValueObject;

namespace Customers.Domain.Entities;

sealed class Customer : Entity
{
    public  string FirstName { get; init; }
    public  string FullName { get; init; }
    public  Email Email { get; init; }

    public Customer(string firstName, string fullName, Email email)
        : base()
    {
        FirstName = firstName;
        FullName = fullName;
        Email = email;
    }

    public Customer(int id, string firstName, string fullName, Email email)
        : base(id)
    {
        FirstName = firstName;
        FullName = fullName;
        Email = email;
    }
}

