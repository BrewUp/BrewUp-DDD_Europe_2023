using Brewup.Purchases.Infrastructure.RabbitMq.Commands;
using Brewup.Purchases.Infrastructure.RabbitMq.Events;
using Brewup.Purchases.Messages.Commands;
using Brewup.Purchases.Messages.Events;
using Brewup.Purchases.ReadModel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace Brewup.Purchases.Infrastructure.RabbitMq;

public static class RabbitMqHelper
{
	public static IServiceCollection AddRabbitMq(this IServiceCollection services, RabbitMqSettings rabbitMqSettings)
	{
		var serviceProvider = services.BuildServiceProvider();
		var repository = serviceProvider.GetRequiredService<IRepository>();
		var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

		var rabbitMQConfiguration = new RabbitMQConfiguration(rabbitMqSettings.Host, rabbitMqSettings.Username, rabbitMqSettings.Password, rabbitMqSettings.ClientId);
		var rabbitMQReference = new RabbitMQReference(rabbitMqSettings.ExchangeCommandName, rabbitMqSettings.QueueCommandName, rabbitMqSettings.ExchangeEventName, rabbitMqSettings.QueueEventName);
		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMQConfiguration, loggerFactory!);
	
		services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMQConfiguration, rabbitMQReference);

		//It's important to build the previous services registrations or IEventBus and IPurchaseOrderService will be null 
		serviceProvider = services.BuildServiceProvider();
		services.AddMufloneRabbitMQConsumers(new List<IConsumer>
		{
			new CreatePurchaseOrderConsumer(repository, mufloneConnectionFactory, rabbitMQReference with { QueueCommandsName = nameof(CreatePurchaseOrder) }, loggerFactory),
			new ChangePurchaseOrderStatusToCompleteConsumer(repository, mufloneConnectionFactory, rabbitMQReference with { QueueCommandsName = nameof(ChangePurchaseOrderStatusToComplete) }, loggerFactory),

			new PurchaseOrderCreatedConsumer(serviceProvider.GetRequiredService<IPurchaseOrderService>(), mufloneConnectionFactory, rabbitMQReference with { QueueEventsName = nameof(PurchaseOrderCreated) }, loggerFactory),
			new PurchaseOrderStatusChangedToCompleteConsumer(serviceProvider.GetRequiredService<IEventBus>(), serviceProvider.GetRequiredService<IPurchaseOrderService>(), mufloneConnectionFactory,
				rabbitMQReference with { QueueEventsName = nameof(PurchaseOrderStatusChangedToComplete) }, loggerFactory)
		});
		return services;
	}
}