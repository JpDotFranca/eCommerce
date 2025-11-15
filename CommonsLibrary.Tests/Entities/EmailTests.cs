using Commons.Library.Entities.ValueObject;

namespace CommonsLibrary.Tests.Entities;

public class EmailTests
{
    [Fact]
    public void Constructor_WithValidEmail_ShouldCreateInstance()
    {
        // Arrange
        string emailAddress = "test@example.com";

        // Act
        Email email = new (emailAddress);

        // Assert
        Assert.Equal(emailAddress, email.Address);
    }

    [Fact]
    public void Constructor_WithNullEmail_ShouldThrowArgumentException()
    {
        // Arrange
        string? emailAddress = null;

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Email(emailAddress!));
        Assert.Equal("address", exception.ParamName);
        Assert.Contains("Invalid email address", exception.Message);
    }

    [Fact]
    public void Constructor_WithEmptyEmail_ShouldThrowArgumentException()
    {
        // Arrange
        string emailAddress = string.Empty;

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Email(emailAddress));
        Assert.Equal("address", exception.ParamName);
        Assert.Contains("Invalid email address", exception.Message);
    }

    [Fact]
    public void Constructor_WithWhitespaceEmail_ShouldThrowArgumentException()
    {
        // Arrange
        string emailAddress = "   ";

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new Email(emailAddress));
        Assert.Equal("address", exception.ParamName);
        Assert.Contains("Invalid email address", exception.Message);
    }

    [Fact]
    public void Constructor_WithoutAtSymbol_ShouldThrowArgumentException()
    {
        // Arrange
        string emailAddress = "testexample.com";

        // Act & Assert
        ArgumentException? exception = Assert.Throws<ArgumentException>(() => new Email(emailAddress));
        Assert.Equal("address", exception.ParamName);
        Assert.Contains("Invalid email address", exception.Message);
    }

    [Fact]
    public void ToString_ShouldReturnEmailAddress()
    {
        // Arrange
        string emailAddress = "test@example.com";
        Email email = new(emailAddress);

        // Act
        string result = email.ToString();

        // Assert
        Assert.Equal(emailAddress, result);
    }

    [Fact]
    public void Equals_WithSameEmail_ShouldReturnTrue()
    {
        // Arrange
        Email email1 = new ("test@example.com");
        Email email2 = new ("test@example.com");

        // Act
        bool result = email1.Equals(email2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_WithSameEmailDifferentCase_ShouldReturnTrue()
    {
        // Arrange
        Email email1 = new ("test@example.com");
        Email email2 = new ("TEST@EXAMPLE.COM");

        // Act
        bool result = email1.Equals(email2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_WithDifferentEmail_ShouldReturnFalse()
    {
        // Arrange
        Email email1 = new ("test@example.com");
        Email email2 = new ("other@example.com");

        // Act
        bool result = email1.Equals(email2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        // Arrange
        Email email = new ("test@example.com");

        // Act
        bool result = email.Equals(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_WithDifferentType_ShouldReturnFalse()
    {
        // Arrange
        Email email = new ("test@example.com");
        string otherObject = "test@example.com";

        // Act
        bool result = email.Equals(otherObject);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetHashCode_WithSameEmail_ShouldReturnSameHashCode()
    {
        // Arrange
        Email email1 = new ("test@example.com");
        Email email2 = new ("test@example.com");

        // Act
        int hash1 = email1.GetHashCode();
        int hash2 = email2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_WithSameEmailDifferentCase_ShouldReturnSameHashCode()
    {
        // Arrange
        Email email1 = new ("test@example.com");
        Email email2 = new ("TEST@EXAMPLE.COM");

        // Act
        int hash1 = email1.GetHashCode();
        int hash2 = email2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_WithDifferentEmail_ShouldReturnDifferentHashCode()
    {
        // Arrange
        Email email1 = new ("test@example.com");
        Email email2 = new ("other@example.com");

        // Act
        int hash1 = email1.GetHashCode();
        int hash2 = email2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }
}
