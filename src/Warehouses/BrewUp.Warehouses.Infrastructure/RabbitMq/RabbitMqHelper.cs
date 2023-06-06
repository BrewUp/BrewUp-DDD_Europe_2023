using BrewUp.Warehouses.Infrastructure.RabbitMq.Commands;
using BrewUp.Warehouses.Infrastructure.RabbitMq.Events;
using BrewUp.Warehouses.ReadModel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouses.Infrastructure.RabbitMq;

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
			new TimeSpan(0, 0, 0, 0, 200),
			rabbitMqSettings.ExchangeCommandName,
			rabbitMqSettings.ExchangeEventName);

		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMQConfiguration, loggerFactory!);

		services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMQConfiguration);

		serviceProvider = services.BuildServiceProvider();
		services.AddMufloneRabbitMQConsumers(new List<IConsumer>
		{
			new BeersReceivedConsumer(serviceProvider.GetRequiredService<IServiceBus>(), mufloneConnectionFactory, loggerFactory),

			new StartBeersReceiveConsumer(serviceProvider.GetRequiredService<IServiceBus>(), serviceProvider.GetRequiredService<ISagaRepository>(), serviceProvider.GetRequiredService<IBeerService>(),  repository!, mufloneConnectionFactory, loggerFactory)
		});

		return services;
	}
}