using Commons.Library.Entities.ValueObject;

namespace Customers.Application.Features.Create;

public class CreateCustomerRequest
{
    public required string FirstName { get; set; }
    public required string FullName { get; set; }
    public required Email Email { get; set; }

    public CreateCustomerCommand Map()
        => new(FirstName, FullName, Email);
}
