using Commons.Library.Entities.ValueObject;

namespace Customers.Domain.Entities;

sealed class Customer : Entity
{
    public string Name { get; init; }
    /// <summary>
    /// Fantasy name.
    /// </summary>
    public string ComercialName { get; init; }
    public Email Email { get; init; }
    public CompanyIdentity CompanyIdentity { get; init; }

    public ContactPerson ContactPerson { get; init; }

    public Customer(string name, string fantasyName, Email email, CompanyIdentity companyIdentity, ContactPerson contactPerson)
        : base()
    {
        Name = name;
        ComercialName = fantasyName;
        Email = email;
        CompanyIdentity = companyIdentity;
        ContactPerson = contactPerson;
    }

    public Customer(int id, string name, string fullName, Email email, CompanyIdentity companyIdentity, ContactPerson contactPerson)
        : base(id)
    {
        Name = name;
        ComercialName = fullName;
        Email = email;
        CompanyIdentity = companyIdentity;
        ContactPerson = contactPerson;

    }
}
 