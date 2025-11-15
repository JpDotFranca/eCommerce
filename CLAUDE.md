# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a .NET 8 eCommerce platform implementing a **Modular Monolith** architecture with Domain-Driven Design (DDD) principles. The solution uses CQRS patterns via MediatR and is designed for eventual migration to microservices through clear module boundaries.

## Solution Structure

- **Solution File**: `eCommerce.slnx` (VS 2022 slim format)
- **.NET Version**: 8.0
- **C# Features**: Latest language version, nullable reference types enabled, implicit usings
- **Build System**: MSBuild with centralized package management via `Directory.Packages.props`

## Core Architecture Patterns

### Modular Organization

Each business domain is a separate module with three projects:

```
Modules/{ModuleName}/
├── {ModuleName}.Contracts/      # Public API (Commands, Queries, DTOs)
├── {ModuleName}/                # Implementation (Domain, Application, Infrastructure)
└── {ModuleName}.Tests/          # Unit and integration tests
```

**Active Modules**:
- **Customers**: Customer accounts, profiles, addresses, preferences, wish lists
- **Catalog**: Products, categories, pricing, inventory, search
- **Orders**: Order lifecycle, cart management, fulfillment
- **Payment**: Payment processing, provider integration, transactions (most developed)
- **Notifications**: Email, SMS, webhooks, templates
- **Pricing**: Promotions, discounts, dynamic pricing
- **Identity**: Authentication, authorization, security

### Internal Module Structure

```
{ModuleName}/
├── Domain/
│   └── Entities/               # Domain entities (sealed classes inheriting from Entity base)
├── Application/
│   ├── Features/              # Feature folders with command handlers
│   └── Repositories/          # Repository interfaces
├── Infrastructure/
│   ├── Database/              # EF Core DbContext and configurations
│   ├── Http/                  # External API clients
│   └── MessageBroker/         # Event publishing/subscribing
└── ModuleSetup.cs             # DI registration extension method
```

### CQRS Pattern

All commands and queries use MediatR:

```csharp
// In {ModuleName}.Contracts/Commands/
public sealed record CreateSomethingCommand(string Param) : IRequest<Result<int>>;

// In {ModuleName}/Application/Features/Create/
internal class CreateSomethingHandler : IRequestHandler<CreateSomethingCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateSomethingCommand command, CancellationToken ct)
    {
        // Implementation using Result<T> pattern
        return Result.Ok(id);
    }
}
```

### DDD Patterns

- **Entities**: Inherit from abstract `Entity` base class, use `sealed` keyword
- **Value Objects**: Immutable types with validation in constructor (see `Commons.Library/Entities/ValueObject/Email.cs`)
- **Repository Pattern**: Interface-first design in domain layer
- **Result Pattern**: Use `FluentResults` instead of exceptions for flow control

## Build and Development Commands

### Building

```bash
# Build entire solution
dotnet build

# Build specific module
dotnet build Modules/Customers/Customers.csproj

# Build with Release configuration
dotnet build -c Release

# Clean and rebuild
dotnet clean && dotnet build
```

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test Modules/Payment/Payment.Tests/Payment.Tests.csproj
dotnet test CommonsLibrary.Tests/CommonsLibrary.Tests.csproj

# Run with code coverage
dotnet test --collect:"XPlat Code Coverage"

# Run architecture tests
dotnet test ArchTests/ArchTests.csproj
```

### Running the Application

```bash
# Run API (with auto-restart on file changes)
dotnet watch --project Hosts/eCommerce.API/eCommerce.API.csproj

# Run API normally
dotnet run --project Hosts/eCommerce.API/eCommerce.API.csproj

# Run Playground console app (for testing commands in isolation)
dotnet run --project Hosts/Playground/Playground.csproj
```

## Key Dependencies

- **MediatR** (13.1.0): CQRS command/query dispatcher
- **FluentResults** (4.0.0): Result<T> pattern for error handling
- **Entity Framework Core** (8.0.0): Data access with SQL Server
- **FluentValidation** (11.9.0): Request validation in MediatR pipeline
- **Serilog** (8.0.0): Structured logging
- **xUnit** (2.5.3): Testing framework
- **NetArchTest.Rules** (1.3.2): Architecture rule enforcement

## Coding Conventions

### Strict Compilation
- **Warnings as Errors**: Enabled (`TreatWarningsAsErrors: true`)
- **Nullable Reference Types**: Enabled globally - all reference types are non-nullable by default
- Use `string?` for nullable strings, `string` for non-null strings

### Code Style
- **Explicit Types**: ALWAYS use explicit type declarations instead of `var`
  - ✅ Correct: `string name = "John";`
  - ✅ Correct: `Customer customer = new Customer();`
  - ✅ Correct: `List<string> items = new();`
  - ❌ Wrong: `var name = "John";`
  - ❌ Wrong: `var customer = new Customer();`
  - This applies to ALL variable declarations, including local variables, parameters, and fields

### Command Handler Pattern
```csharp
// Commands should be records in Contracts project
public sealed record CreateCustomerCommand(string Name, string Email)
    : IRequest<Result<int>>;

// Handlers should be internal in main module
internal class CreateCustomerHandler
    : IRequestHandler<CreateCustomerCommand, Result<int>>
{
    public async Task<Result<int>> Handle(
        CreateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        // Use Result.Ok() or Result.Fail() instead of throwing exceptions
        return Result.Ok(customerId);
    }
}
```

### Module Initialization
Each module exposes a registration method:

```csharp
// In {ModuleName}/ModuleSetup.cs
public static class ModuleSetup
{
    public static IServiceCollection Add{ModuleName}Module(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register module dependencies
        return services;
    }
}

// In Hosts/eCommerce.API/Program.cs
builder.Services.AddPaymentModule(configuration);
```

### Entity Pattern
```csharp
public sealed class Customer : Entity
{
    // Private constructor for EF Core
    private Customer() { }

    // Public factory method or constructor with validation
    public Customer(string name, Email email)
    {
        // Validation and initialization
    }
}
```

### Value Objects
Place shared value objects in `Commons.Library/Entities/ValueObject/`. Example:

```csharp
public class Email
{
    public string Address { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email cannot be null or empty");
        if (!address.Contains("@"))
            throw new ArgumentException("Invalid email format");

        Address = address;
    }

    public override bool Equals(object? obj) { /* value equality */ }
    public override int GetHashCode() { /* hash based on value */ }
}
```

## Testing Strategy

### Test Organization
- **Unit Tests**: Fast, isolated tests for domain logic and handlers
- **Integration Tests**: Database and infrastructure tests (use separate folder in test project)
- **Architecture Tests**: `ArchTests/` project enforces architectural rules using NetArchTest

### Test Structure
```csharp
public class EmailTests
{
    [Fact]
    public void Constructor_WithValidEmail_ShouldCreateInstance()
    {
        // Arrange
        string emailAddress = "test@example.com";

        // Act
        Email email = new Email(emailAddress);

        // Assert
        Assert.Equal(emailAddress, email.Address);
    }
}
```

## Common Development Patterns

### Adding a New Feature to a Module

1. Define command/query in `{ModuleName}.Contracts/Commands/` or `Queries/`
2. Create feature folder in `{ModuleName}/Application/Features/{FeatureName}/`
3. Implement handler class in the feature folder
4. Add repository interface if needed in `Application/Repositories/`
5. Implement infrastructure in `Infrastructure/` layer
6. Write tests in `{ModuleName}.Tests/`
7. Register dependencies in `ModuleSetup.cs` if needed

### Adding a New Module

1. Create three projects following the naming convention:
   - `{ModuleName}.Contracts`
   - `{ModuleName}`
   - `{ModuleName}.Tests`
2. Add projects to `eCommerce.slnx`
3. Create `ModuleSetup.cs` with registration method
4. Follow the folder structure: Domain/, Application/, Infrastructure/
5. Add module registration to API host
6. Document module purpose in README.md

## Important Notes

- **Module Communication**: Modules should communicate through Contracts projects only, never directly reference other module implementations
- **Infrastructure Concerns**: Database, HTTP clients, and message brokers go in Infrastructure/ folder
- **Feature Organization**: Group related handlers in feature folders (e.g., `Features/Pix/CreateCharge/`)
- **Immutability**: Prefer `record` types for DTOs and commands
- **Sealed Classes**: Domain entities should be `sealed` to prevent inheritance
- **Result Pattern**: Always return `Result<T>` from handlers, use `Result.Ok()` and `Result.Fail()` instead of throwing exceptions for business logic errors

## Project Status

The codebase is in active development with the module structure established. The Payment and Customers modules have the most developed implementations. The API host (`Hosts/eCommerce.API/`) currently has template code and needs module integration.
