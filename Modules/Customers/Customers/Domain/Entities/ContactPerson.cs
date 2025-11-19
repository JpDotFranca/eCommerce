using Commons.Library.Entities.ValueObject;

namespace Customers.Domain.Entities;

public sealed class ContactPerson : Entity
{
    public string FirstName { get; init; }
    public string FullName { get; init; }
    public Email Email { get; init; }
    public string PhoneNumber { get; init; }

    private ContactPerson() : base() { }

    public ContactPerson(string firstName, string fullName, Email email, string phoneNumber)
        :base()
    {
        FirstName = firstName;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public ContactPerson(int id, string firstName, string fullName, Email email, string phoneNumber)
        : base(id)
    {
        FirstName = firstName;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

}
 