using FluentResults;
using MediatR;

namespace Payment.Features.Pix.CreateCharge;

record CreateChargeCommand(string ProductName,
                           decimal Amount,
                           CustomerDto Customer) : IRequest<Result<CreateChargeResponse>>;

record CustomerDto(string FirstName, string LastName, string Email);

record CreateChargeResponse(int ChargeId, string ProductName, decimal Amount);
