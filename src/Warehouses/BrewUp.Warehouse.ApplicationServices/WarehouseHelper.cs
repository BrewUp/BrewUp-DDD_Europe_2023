using BrewUp.Warehouse.ApplicationServices.Abstracts;
using BrewUp.Warehouse.ApplicationServices.Concretes;
using BrewUp.Warehouse.ApplicationServices.Endpoints;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouse.ApplicationServices;

public static class WarehouseHelper
{
    public static IServiceCollection AddWarehouseServices(this IServiceCollection services)
    {
        services.AddScoped<IWarehouseOrchestrator, WarehouseOrchestrator>();
        services.AddScoped<IWarehouseAvailabilityService, WarehouseAvailabilityService>();
        
        return services;
    }
}