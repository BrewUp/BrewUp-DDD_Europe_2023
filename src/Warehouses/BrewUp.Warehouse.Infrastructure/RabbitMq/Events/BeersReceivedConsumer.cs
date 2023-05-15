using BrewUp.Warehouse.Messages.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouse.Infrastructure.RabbitMq.Events;

public sealed class BeersReceivedConsumer : IntegrationEventsConsumerBase<BeersReceived>
{
	protected override IEnumerable<IIntegrationEventHandlerAsync<BeersReceived>> HandlersAsync { get; }

	public BeersReceivedConsumer(IServiceProvider serviceProvider,
		RabbitMQReference rabbitMQReference,
		IMufloneConnectionFactory mufloneConnectionFactory,
		ILoggerFactory loggerFactory) : base(rabbitMQReference, mufloneConnectionFactory, loggerFactory)
	{
		HandlersAsync = serviceProvider.GetServices<IIntegrationEventHandlerAsync<BeersReceived>>();
	}
}