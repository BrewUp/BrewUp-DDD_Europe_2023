using Brewup.Purchases.ApplicationService.Validators;
using Brewup.Purchases.Messages.Events;
using Brewup.Purchases.ReadModel.EventHandlers;
using Brewup.Purchases.ReadModel.Services;
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

		services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
		services.AddScoped<IPurchasesOrchestrator, PurchasesOrchestrator>();
		services.AddScoped<IDomainEventHandlerAsync<PurchaseOrderCreated>, PurchaseOrderCreatedEventHandler>();

		return services;
	}
}