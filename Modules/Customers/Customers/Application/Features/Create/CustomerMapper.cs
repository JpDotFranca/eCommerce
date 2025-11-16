using Customers.Domain.Entities;

namespace Customers.Application.Features.Create;

static class CustomerMapper
{
    public static Customer Map(this CreateCustomerCommand createCustomerCommand)
         => new Customer(createCustomerCommand.Name,
                         createCustomerCommand.ComercialName,
                         createCustomerCommand.Email,
                         createCustomerCommand.CompanyIdentity,
                         createCustomerCommand.ContactPerson.Map());
}
