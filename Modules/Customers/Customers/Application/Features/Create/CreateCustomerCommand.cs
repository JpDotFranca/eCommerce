using Commons.Library.Entities.ValueObject;
using FluentResults;
using MediatR;

namespace Customers.Application.Features.Create;

public record CreateCustomerCommand
    (string FirstName, string FullName, Email Email) : IRequest<Result<int>>;
