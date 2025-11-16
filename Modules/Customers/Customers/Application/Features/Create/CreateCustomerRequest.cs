using Commons.Library.Entities.ValueObject;
using System.Text.Json.Serialization;

namespace Customers.Application.Features.Create;

public class CreateCustomerRequest
{
    [JsonIgnore] private CompanyIdentity _companyIdentity { get => CompanyIdentity; }
    
    public required string FirstName { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string CompanyIdentity { get; set; }
    public required ContactPersonDTO ContactPerson { get; set; }

    public CreateCustomerCommand Map()
        => new(FirstName, FullName, _companyIdentity, Email, ContactPerson);
}
