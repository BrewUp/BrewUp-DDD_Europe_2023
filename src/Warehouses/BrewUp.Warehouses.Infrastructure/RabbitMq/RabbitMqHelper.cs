using BrewUp.Warehouses.Infrastructure.RabbitMq.Commands;
using BrewUp.Warehouses.Infrastructure.RabbitMq.Events;
using BrewUp.Warehouses.Messages.Commands;
using BrewUp.Warehouses.Messages.Events;
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
			rabbitMqSettings.ClientId);
		var rabbitMQReference = new RabbitMQReference(rabbitMqSettings.ExchangeCommandName,
			rabbitMqSettings.QueueCommandName, rabbitMqSettings.ExchangeEventName, rabbitMqSettings.QueueEventName);
		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMQConfiguration, loggerFactory!);

		services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMQConfiguration, rabbitMQReference);

		serviceProvider = services.BuildServiceProvider();
		services.AddMufloneRabbitMQConsumers(new List<IConsumer>
		{
			new BeersReceivedConsumer(serviceProvider.GetRequiredService<IServiceBus>(), mufloneConnectionFactory, rabbitMQReference with { QueueEventsName = nameof(BeersReceived) }, loggerFactory),

			new StartBeersReceiveConsumer(serviceProvider.GetRequiredService<IServiceBus>(), serviceProvider.GetRequiredService<ISagaRepository>(), repository!, mufloneConnectionFactory, rabbitMQReference with { QueueCommandsName = nameof(StartBeersReceivedSaga) }, loggerFactory),

			new CreateBeerConsumer(repository!, mufloneConnectionFactory, rabbitMQReference with { QueueCommandsName = nameof(CreateBeer) }, loggerFactory),
			new BeerCreatedConsumer(serviceProvider.GetRequiredService<IServiceBus>(), serviceProvider.GetRequiredService<ISagaRepository>(), serviceProvider.GetRequiredService<IBeerService>(), mufloneConnectionFactory, rabbitMQReference with { QueueEventsName = nameof(BeerCreated) }, loggerFactory),

			new LoadBeerInStockConsumer(repository!, mufloneConnectionFactory, rabbitMQReference with { QueueCommandsName = nameof(LoadBeerInStock)}, loggerFactory),
			new BeerLoadedInStockConsumer( serviceProvider.GetRequiredService<IServiceBus>(),  serviceProvider.GetRequiredService<ISagaRepository>(), serviceProvider.GetRequiredService<IBeerService>(), mufloneConnectionFactory, rabbitMQReference with { QueueEventsName = nameof(BeerLoadedInStock) }, loggerFactory)
		});

		return services;
	}
}