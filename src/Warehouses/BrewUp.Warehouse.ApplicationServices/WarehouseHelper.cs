using BrewUp.Warehouse.ApplicationServices.Abstracts;
using BrewUp.Warehouse.ApplicationServices.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouse.ApplicationServices;

public static class WarehouseHelper
{
    public static IServiceCollection AddWarehouseServices(this IServiceCollection services)
    {
        services.AddScoped<IWarehouseOrchestrator, WarehouseOrchestrator>();
        
        return services;
    }
}