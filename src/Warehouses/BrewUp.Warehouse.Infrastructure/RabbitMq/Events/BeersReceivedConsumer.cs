using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.ReadModel.EventHandlers;
using BrewUp.Warehouse.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouse.Infrastructure.RabbitMq.Events;

public sealed class BeersReceivedConsumer : IntegrationEventsConsumerBase<BeersReceived>
{
	protected override IEnumerable<IIntegrationEventHandlerAsync<BeersReceived>> HandlersAsync { get; }

	public BeersReceivedConsumer(IBeerService beerService, IMufloneConnectionFactory mufloneConnectionFactory,
		RabbitMQReference rabbitMQReference, ILoggerFactory loggerFactory) : base(mufloneConnectionFactory,
		rabbitMQReference, loggerFactory)
	{
		HandlersAsync = new List<IIntegrationEventHandlerAsync<BeersReceived>>()
		{
			new BeersReceivedEventHandler(loggerFactory, beerService)
		};
	}
}