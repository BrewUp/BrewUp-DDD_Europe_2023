using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
				rabbitMqSettings.ExchangeCommandName);
		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMQConfiguration, loggerFactory!);

		var consumers = new List<IConsumer>
		{

		};

		services.AddMufloneTransportRabbitMQ(rabbitMQConfiguration, rabbitMQReference, consumers);

		return services;
	}
}