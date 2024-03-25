using Microsoft.Extensions.DependencyInjection;

namespace SellingDreamsInfrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<InfrastructureDbContext>();
        return services;
    }
}