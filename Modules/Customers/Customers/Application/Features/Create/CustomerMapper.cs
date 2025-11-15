using Customers.Domain.Entities;

namespace Customers.Application.Features.Create;

static class CustomerMapper
{
   public static Customer Map(this CreateCustomerCommand createCustomerCommand)
        => new Customer(createCustomerCommand.FirstName, 
                        createCustomerCommand.FullName, 
                        createCustomerCommand.Email);
}
