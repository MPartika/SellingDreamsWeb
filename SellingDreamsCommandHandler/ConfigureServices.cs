using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SellingDreamsCommandHandler;

public static class ConfigureServices
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        var assembly = AppDomain.CurrentDomain.Load("SellingDreamsCommandHandler");
        RegisterHandler(services, assembly);
        return services;
    }

    private static void RegisterHandler(IServiceCollection services, Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
        {
            foreach (var appliedInterface in type.GetInterfaces().Where(i => i.IsGenericType && (
                    i.GetGenericTypeDefinition() == typeof(ICommandHandlerAsync<,>) ||
                    i.GetGenericTypeDefinition() == typeof(ICommandHandlerAsync<>) ||
                    i.GetGenericTypeDefinition() == typeof(ICommandHandlerListAsync<,>) ||
                    i.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                    i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                    i.GetGenericTypeDefinition() == typeof(ICommandHandlerList<,>)
            )))
            {
                services.AddTransient(appliedInterface, type);
            }
        }
    }
}
