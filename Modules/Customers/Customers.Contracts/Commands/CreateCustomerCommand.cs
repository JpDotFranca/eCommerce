using FluentResults;
using MediatR;

namespace Customers.Contracts.Commands;

public sealed

record CreateCustomerCommand(string Name, string Email):

IRequest<Result<int>>;
