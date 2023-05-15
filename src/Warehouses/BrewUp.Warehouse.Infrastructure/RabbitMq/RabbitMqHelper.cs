using BrewUp.Warehouse.Infrastructure.RabbitMq.Commands;
using BrewUp.Warehouse.Infrastructure.RabbitMq.Events;
using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.ReadModel.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouse.Infrastructure.RabbitMq;

public static class RabbitMqHelper
{
	public static IServiceCollection AddRabbitMq(this IServiceCollection services,
		RabbitMqSettings rabbitMqSettings)
	{
		services.AddScoped<IIntegrationEventHandlerAsync<BeersReceived>, BeersReceivedEventHandler>();

		services.AddSingleton<IServiceBus, ServiceBus>();

		var serviceProvider = services.BuildServiceProvider();
		var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
		var repository = serviceProvider.GetService<IRepository>();

		var rabbitMQConfiguration = new RabbitMQConfiguration(
			rabbitMqSettings.Host,
			rabbitMqSettings.Username,
			rabbitMqSettings.Password,
			rabbitMqSettings.ClientId);
		var rabbitMQReference =
			new RabbitMQReference(rabbitMqSettings.ExchangeCommandName,
				rabbitMqSettings.QueueCommandName,
				rabbitMqSettings.ExchangeEventName,
				rabbitMqSettings.QueueEventName);
		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMQConfiguration, loggerFactory!);

		var consumers = new List<IConsumer>
		{
			new CreateBeerConsumer(repository!, rabbitMQReference with { QueueCommandsName = nameof(CreateBeer)}, mufloneConnectionFactory, loggerFactory!),
			new BeersReceivedConsumer(serviceProvider, rabbitMQReference with { QueueEventsName = nameof(BeersReceived)}, mufloneConnectionFactory, loggerFactory!)
		};

		services.AddMufloneTransportRabbitMQ(rabbitMQConfiguration, rabbitMQReference, consumers);

		return services;
	}
}