using Customers.Application.Repositories;
using Customers.Domain.Entities;
using FluentResults;

namespace Customers.Infraestructure.Database;

class CustomerRepository : ICustomerRepository
{
    public async Task<Result<Customer>> Add(Customer customer)
    {
        return await Task.FromResult(Result.Ok(customer));
    }
}
 
