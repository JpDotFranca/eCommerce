using Commons.Library.Repositories;
using Customers.Domain.Entities;
using FluentResults;

namespace Customers.Application.Repositories;

interface ICustomersRepository : IRepository
{
    Task<Result<Customer>> Add(Customer customer);
}