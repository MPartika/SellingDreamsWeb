
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SellingDreamsService;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        var assemblyServices = Assembly.GetExecutingAssembly();
        var typesServices = assemblyServices
            .GetTypes()
            .Where(type => type.GetInterfaces().Contains(typeof(IDependency)))
            .ToList();
        typesServices.ForEach(type =>
        {
            var serviceType = type.GetInterfaces().FirstOrDefault(i => i != typeof(IDependency));
            if (serviceType != null)
                services.AddTransient(serviceType, type);
        });

        return services;
    }
}