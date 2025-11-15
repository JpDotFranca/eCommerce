using Asp.Versioning;
using Asp.Versioning.Builder;
using Commons.Library.Extensions.ModuleRegistration;
using Customers.Application.Features.Create;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customers;

public static class CustomersModule
{
    public static void AddCustomersEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        ApiVersionSet apiVersionSet = routeBuilder.NewApiVersionSet()
             .HasApiVersion(new ApiVersion(1, 0)) 
             .ReportApiVersions()
             .Build();

        RouteGroupBuilder group = routeBuilder
            .MapGroup("api/v{version:apiVersion}/customers")
            .WithApiVersionSet(apiVersionSet)
            .WithTags("Customers");

        group.MapPost("/", async (CreateCustomerRequest request, ISender sender) =>
        {
            CreateCustomerCommand command = request.Map();

            Result<int> createResult = await sender.Send(command);

            return createResult.Return();
        }).WithName("CreateCustomer")
          .WithOpenApi();
    }

    public static IServiceCollection AddCustomersModule(this IServiceCollection services, IConfiguration configuration)
        => services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ModuleRegister).Assembly))
                   .RegisterRepositories(typeof(ModuleRegister).Assembly);
}
