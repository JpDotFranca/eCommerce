namespace Commons.Library.Entities.ValueObject;

public class Email
{
    public string Address { get; }
    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !address.Contains("@"))
        {
            throw new ArgumentException("Invalid email address.", nameof(address));
        }
        Address = address;
    }
    public override string ToString() => Address;
    public override bool Equals(object? obj)
    {
        if (obj is Email other)
        {
            return Address.Equals(other.Address, StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }
    public override int GetHashCode() => Address.GetHashCode(StringComparison.OrdinalIgnoreCase);

    public static implicit operator string(Email email) => email.Address;
    public static implicit operator Email(string email) => new Email(email);
}
