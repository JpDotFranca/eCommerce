using Commons.Library.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Commons.Library.Extensions.ModuleRegistration;

public static class ModuleRegister
{
    /// <summary>
    /// Registers all repository interfaces and their implementations from the specified assembly as scoped.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleAssembly"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterRepositories(this IServiceCollection services, Assembly moduleAssembly)
    {
        IEnumerable<Type> repositoryInterfaces = moduleAssembly.GetTypes()
            .Where(t => t.IsInterface && typeof(IRepository).IsAssignableFrom(t) && t != typeof(IRepository));

        foreach (Type interfaceType in repositoryInterfaces)
        {
            Type? implementationType = moduleAssembly.GetTypes()
               .FirstOrDefault(t => t.IsClass && !t.IsAbstract && interfaceType.IsAssignableFrom(t));

            if (implementationType != null)
            {
                services.AddScoped(interfaceType, implementationType);
            }
        }

        return services;
    }
}
