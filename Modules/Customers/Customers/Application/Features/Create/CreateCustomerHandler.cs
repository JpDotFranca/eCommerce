using Customers.Application.Repositories;
using Customers.Domain.Entities;
using FluentResults;
using MediatR;

namespace Customers.Application.Features.Create;

class CreateCustomerHandler(ICustomerRepository CustomerRepository)
    : IRequestHandler<CreateCustomerCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateCustomerCommand createCustomerCommand, CancellationToken cancellationToken)
    {
        Customer customer = createCustomerCommand.Map();
        Result<Customer> customerAdded = await CustomerRepository.Add(customer);

        return Result.Ok(customerAdded.Value.Id);
    }
}
