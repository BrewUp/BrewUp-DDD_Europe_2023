using Brewup.Purchases.Infrastructure.RabbitMq;
using Brewup.Purchases.Infrastructure.RabbitMq.Commands;
using Brewup.Purchases.Infrastructure.RabbitMq.Events;
using Brewup.Purchases.Messages.Commands;
using Brewup.Purchases.Messages.Events;
using EventStore.ClientAPI.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace Brewup.Purchases.Infrastructure;

public static class RabbitMqHelper
{
	public static IServiceCollection AddRabbitMq(this IServiceCollection services,
		RabbitMqSettings rabbitMqSettings)
	{
		var serviceProvider = services.BuildServiceProvider();
		var repository = serviceProvider.GetService<IRepository>();
		var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

		var rabbitMQConfiguration = new RabbitMQConfiguration(
			rabbitMqSettings.Host,
			rabbitMqSettings.Username,
			rabbitMqSettings.Password,
			rabbitMqSettings.ClientId);
		var rabbitMQReference = new RabbitMQReference(rabbitMqSettings.ExchangeCommandName,
			rabbitMqSettings.QueueCommandName,
			rabbitMqSettings.ExchangeEventName,
			rabbitMqSettings.QueueEventName);

		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMQConfiguration, loggerFactory!);

		services.AddMufloneTransportRabbitMQ(rabbitMQConfiguration, rabbitMQReference, new List<IConsumer>
		{
			new CreatePurchaseOrderConsumer(repository, mufloneConnectionFactory,
				rabbitMQReference with { QueueCommandsName = nameof(CreatePurchaseOrder) }, loggerFactory),
			new PurchaseOrderCreatedConsumer(serviceProvider, mufloneConnectionFactory,
				rabbitMQReference with { QueueEventsName = nameof(PurchaseOrderCreated) }, loggerFactory),
			new ChangePurchaseOrderStatusToCompleteConsumer(repository, mufloneConnectionFactory,
				rabbitMQReference with { QueueCommandsName = nameof(ChangePurchaseOrderStatusToComplete) }, loggerFactory),
			new PurchaseOrderStatusChangedToCompleteConsumer(serviceProvider, mufloneConnectionFactory,
				rabbitMQReference with { QueueEventsName = nameof(PurchaseOrderStatusChangedToComplete) }, loggerFactory)
		});

		return services;
	}
}