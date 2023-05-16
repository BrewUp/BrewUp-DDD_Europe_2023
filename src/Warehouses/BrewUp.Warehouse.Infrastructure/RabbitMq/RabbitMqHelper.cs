using BrewUp.Warehouse.Infrastructure.RabbitMq.Commands;
using BrewUp.Warehouse.Infrastructure.RabbitMq.Events;
using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouse.Messages.Events;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouse.Infrastructure.RabbitMq;

public static class RabbitMqHelper
{
	public static IServiceCollection AddRabbitMq(this IServiceCollection services,
		RabbitMqSettings rabbitMqSettings)
	{
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
		services.AddMufloneTransportRabbitMQ(rabbitMQConfiguration, rabbitMQReference);

		var serviceProvider = services.BuildServiceProvider();

		var consumers = new List<IConsumer>
		{
			new BeersReceivedConsumer(serviceProvider, rabbitMQReference with { QueueEventsName = nameof(BeersReceived)}),

			new CreateBeerConsumer(serviceProvider, rabbitMQReference with { QueueCommandsName = nameof(CreateBeer)}),
			new BeerCreatedConsumer(serviceProvider, rabbitMQReference with{ QueueEventsName = nameof(BeerCreated)})
		};

		services.RegisterConsumersInTransportRabbitMQ(consumers);

		return services;
	}
}