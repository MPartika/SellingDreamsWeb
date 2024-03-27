using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SellingDreamsCommandHandler;

public static class ConfigureServices
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        var assembly = AppDomain.CurrentDomain.Load("SellingDreamsCommandHandler");
        RegisterHandler(services, assembly, typeof(ICommandHandlerListAsync<,>));
        RegisterHandler(services, assembly, typeof(ICommandHandlerAsync<,>));
        RegisterHandler(services, assembly, typeof(ICommandHandlerAsync<>));
        RegisterHandler(services, assembly, typeof(ICommandHandler<>));
        RegisterHandler(services, assembly, typeof(ICommandHandler<,>));
        RegisterHandler(services, assembly, typeof(ICommandHandlerList<,>));
        return services;
    }

    private static void RegisterHandler(IServiceCollection services, Assembly assembly, Type handlerTypeListAsync)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsClass && !type.IsAbstract)
            {
                foreach (var i in type.GetInterfaces())
                {
                    if (i.IsGenericType && i.GetGenericTypeDefinition() == handlerTypeListAsync)
                    {
                        services.AddTransient(i, type);
                    }
                }
            }
        }
    }
}
