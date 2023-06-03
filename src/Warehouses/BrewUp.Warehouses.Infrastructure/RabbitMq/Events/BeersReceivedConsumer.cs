using BrewUp.Warehouses.Messages.Events;
using BrewUp.Warehouses.Sagas.Adapters;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructure.RabbitMq.Events;

public sealed class BeersReceivedConsumer : IntegrationEventsConsumerBase<BeersReceived>
{
	protected override IEnumerable<IIntegrationEventHandlerAsync<BeersReceived>> HandlersAsync { get; }

	public BeersReceivedConsumer(IServiceBus serviceBus, IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory) 
		: base(connectionFactory, loggerFactory)
	{
		HandlersAsync = new List<IIntegrationEventHandlerAsync<BeersReceived>>()
		{
			new BeersReceivedEventHandler(loggerFactory, serviceBus)
		};
	}
}