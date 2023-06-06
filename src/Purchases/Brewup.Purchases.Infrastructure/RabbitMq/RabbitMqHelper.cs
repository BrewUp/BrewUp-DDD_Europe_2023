using Brewup.Purchases.Infrastructure.RabbitMq.Commands;
using Brewup.Purchases.Infrastructure.RabbitMq.Events;
using Brewup.Purchases.ReadModel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

		var rabbitMQConfiguration = new RabbitMQConfiguration(
			rabbitMqSettings.Host,
			rabbitMqSettings.Username,
			rabbitMqSettings.Password,
			new TimeSpan(0, 0, 0, 0, 200),
			rabbitMqSettings.ExchangeCommandName,
			rabbitMqSettings.ExchangeEventName);
		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMQConfiguration, loggerFactory!);

		services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMQConfiguration);

		//It's important to build the previous services registrations or IEventBus and IPurchaseOrderService will be null 
		//We need to improve this part. It's awful like it is right now
		serviceProvider = services.BuildServiceProvider();
		services.AddMufloneRabbitMQConsumers(new List<IConsumer>
		{
			new CreatePurchaseOrderConsumer(repository, mufloneConnectionFactory, loggerFactory),

			new PurchaseOrderCreatedConsumer(serviceProvider.GetRequiredService<IPurchaseOrderService>(), mufloneConnectionFactory, loggerFactory)
		});
		return services;
	}
}