using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Payment;

public static class ModuleSetup
{
    public static IServiceCollection AddPaymentModule(this IServiceCollection services, IConfiguration configuration)
        => services;
}
