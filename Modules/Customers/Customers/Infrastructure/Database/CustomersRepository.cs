using Customers.Application.Repositories;
using Customers.Domain.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Customers.Infrastructure.Database;

class CustomersRepository : ICustomersRepository
{
    private CustomersDbContext _context;
    public CustomersRepository(CustomersDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Customer>> Add(Customer customer)
    {
        EntityEntry<Customer> addedCustomer = await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return await Task.FromResult(Result.Ok(customer));
    }
}

