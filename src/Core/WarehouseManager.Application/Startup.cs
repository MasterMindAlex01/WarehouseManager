using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WarehouseManager.Application;

public static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly))
            .AddValidatorsFromAssembly(assembly);
    }
}