using Commons.Library.Entities.ValueObject;
using Customers.Domain.Entities;
using FluentResults;
using MediatR;

namespace Customers.Application.Features.Create;

public record CreateCustomerCommand
    (string Name, string ComercialName, CompanyIdentity CompanyIdentity, Email Email, ContactPersonDTO ContactPerson) : IRequest<Result<int>>;

public class ContactPersonDTO
{
    public required string FirstName { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }

    public ContactPerson Map()
    {
        return new ContactPerson(FirstName, FullName, Email, PhoneNumber);
    }

}