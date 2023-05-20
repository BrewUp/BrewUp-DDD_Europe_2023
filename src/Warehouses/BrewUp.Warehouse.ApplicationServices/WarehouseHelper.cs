using BrewUp.Warehouse.ApplicationServices.Adapters;
using BrewUp.Warehouse.ApplicationServices.Validators;
using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.ReadModel.EventHandlers;
using BrewUp.Warehouse.ReadModel.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Events;

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
		services.AddScoped<IBeerService, BeerService>();

		services.AddScoped<IIntegrationEventHandlerAsync<BeersReceived>, BeersReceivedEventHandler>();
		services.AddScoped<IDomainEventHandlerAsync<BeerCreated>, BeerCreatedEventHandler>();

		return services;
	}
}