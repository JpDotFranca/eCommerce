namespace Commons.Library.Entities.ValueObject;

public class CompanyIdentity
{
    public string Identifier { get; }
    public CompanyIdentity(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier))
        {
            throw new ArgumentException("Invalid company identifier.", nameof(identifier));
        }

        if(identifier.Length < 14)
        {
            throw new ArgumentException("Company identifier must be at least 14 characters long.", nameof(identifier));
        }

        Identifier = identifier;
    }
    public override string ToString() => Identifier;
    public override bool Equals(object? obj)
    {
        if (obj is CompanyIdentity other)
        {
            return Identifier.Equals(other.Identifier, StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }
    public override int GetHashCode() => Identifier.GetHashCode(StringComparison.OrdinalIgnoreCase);
    public static implicit operator string(CompanyIdentity identity) => identity.Identifier;
    public static implicit operator CompanyIdentity(string identifier) => new CompanyIdentity(identifier);
}