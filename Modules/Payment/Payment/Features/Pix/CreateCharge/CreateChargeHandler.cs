using FluentResults;
using MediatR;

namespace Payment.Features.Pix.CreateCharge;

internal class CreateChargeHandler : IRequestHandler<CreateChargeCommand, Result<CreateChargeResponse>>
{
    public async Task<Result<CreateChargeResponse>> Handle(CreateChargeCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(4), cancellationToken);

        return Result.Ok(new CreateChargeResponse(
            ChargeId: 1,
            ProductName: request.ProductName,
            Amount: request.Amount));
    }
}
