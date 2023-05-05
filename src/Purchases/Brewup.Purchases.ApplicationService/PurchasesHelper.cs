using Brewup.Purchases.ApplicationService.Abstracts;
using Brewup.Purchases.ApplicationService.Concretes;
using Brewup.Purchases.ApplicationService.EventHandlers;
using Brewup.Purchases.ApplicationService.Validators;
using Brewup.Purchases.Messages.Events;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Events;

namespace Brewup.Purchases.ApplicationService;

public static class PurchasesHelper
{
	public static IServiceCollection AddPurchases(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<OrderValidator>();

		services.AddSingleton<ValidationHandler>();

		services.AddScoped<IPurchasesOrchestrator, PurchasesOrchestrator>();
		services.AddScoped<IDomainEventHandlerAsync<BuyOrderCreated>, BuyOrderCreatedEventHandler>();

		return services;
	}
}