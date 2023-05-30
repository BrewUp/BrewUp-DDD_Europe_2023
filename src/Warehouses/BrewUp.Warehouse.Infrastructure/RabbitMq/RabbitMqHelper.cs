﻿using BrewUp.Warehouse.Infrastructure.RabbitMq.Commands;
using BrewUp.Warehouse.Infrastructure.RabbitMq.Events;
using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.ReadModel.Services;
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

			new CreateBeerConsumer(repository!, mufloneConnectionFactory, rabbitMQReference with { QueueCommandsName = nameof(CreateBeer) }, loggerFactory),
			new BeerCreatedConsumer(serviceProvider.GetRequiredService<IBeerService>(), mufloneConnectionFactory, rabbitMQReference with { QueueEventsName = nameof(BeerCreated) }, loggerFactory)
		});

		return services;
	}
}