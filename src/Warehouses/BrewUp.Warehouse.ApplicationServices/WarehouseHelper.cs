using BrewUp.Warehouse.ApplicationServices.Validators;
using BrewUp.Warehouse.ReadModel.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouse.ApplicationServices;

public static class WarehouseHelper
{
	public static IServiceCollection AddWarehouseServices(this IServiceCollection services)
	{
		services.AddSingleton<ValidationHandler>();

		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<BeersReceivedValidator>();

		services.AddScoped<IWarehouseOrchestrator, WarehouseOrchestrator>();
		services.AddScoped<IWarehouseAvailabilityService, WarehouseAvailabilityService>();

		return services;
	}
}