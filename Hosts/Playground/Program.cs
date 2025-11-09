using FluentResults;
using MediatR;

public partial class Program
{
    static async Task Main(string[] args)
    {
        // 1. Configure DI
        ServiceCollection services = new();

        // Register MediatR handlers from this assembly (commands + notifications + handlers)
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

        // Build provider
        using ServiceProvider provider = services.BuildServiceProvider();

        IMediator mediator = provider.GetRequiredService<IMediator>();

        // 2. Send a command through MediatR to invoke the handler (which will publish events)
        CreateOrderCommand command = new("ExampleProduct", 19.99m);
        Result<CreateOrderResult> result;
        try
        {
            result = await mediator.Send(command);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unhandled exception: " + ex.Message);
            return;
        }

        // 3. Inspect the FluentResults result
        if (result.IsSuccess)
        {
            CreateOrderResult order = result.Value;
            Console.WriteLine("Order created (Main): Id=" + order.Id + ", Product=" + order.ProductName + ", Value=" + order.Value);
            // Optionally, you could also publish events from the caller side:
            // await mediator.Publish(new OrderCreatedEvent(order.Id, order.ProductName, order.Value));
        }
        else
        {
            Console.WriteLine("Failed to create order. Errors:");
            foreach (IError? error in result.Errors)
            {
                Console.WriteLine("- " + error.Message);
            }
        }
    }
}

public record CreateOrderCommand(string ProductName, decimal Value) : IRequest<Result<CreateOrderResult>>;
public record CreateOrderResult(int Id, string ProductName, decimal Value);
public record OrderCreatedEvent(int Id, string ProductName, decimal Value) : INotification;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Result<CreateOrderResult>>
{
    private readonly IMediator _mediator;

    public CreateOrderHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Result<CreateOrderResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Simulate work
        await Task.Delay(1000, cancellationToken);
        Console.WriteLine("Order created inside handler");

        CreateOrderResult order = new (12, request.ProductName, request.Value);

        // Publish a domain/event notification so other parts of the system can react
        await _mediator.Publish(new OrderCreatedEvent(order.Id, order.ProductName, order.Value), cancellationToken);

        return Result.Ok(order);
    }
}

// Notification handler #1 - simulate sending confirmation email
public class SendOrderConfirmationHandler : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[Email] Sending confirmation for order {notification.Id} ({notification.ProductName})");
        // Simulate async work if needed
        return Task.CompletedTask;
    }
}

// Notification handler #2 - simulate updating inventory
public class UpdateInventoryHandler : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[Inventory] Decrementing stock for {notification.ProductName}");
        // Simulate async work if needed
        return Task.CompletedTask;
    }
}

