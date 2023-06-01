using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.ReadModel.EventHandlers;
using BrewUp.Warehouse.ReadModel.Services;
using BrewUp.Warehouses.Sagas.Sagas;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouse.Infrastructure.RabbitMq.Events;

public sealed class BeerCreatedConsumer : DomainEventsConsumerBase<BeerCreated>
{
	protected override IEnumerable<IDomainEventHandlerAsync<BeerCreated>> HandlersAsync { get; }

	public BeerCreatedConsumer(IServiceBus serviceBus,
		ISagaRepository sagaRepository,
		IBeerService beerService,
		IMufloneConnectionFactory mufloneConnectionFactory,
		RabbitMQReference rabbitMQReference,
		ILoggerFactory loggerFactory) : base(mufloneConnectionFactory, rabbitMQReference, loggerFactory)
	{
		HandlersAsync = new List<IDomainEventHandlerAsync<BeerCreated>>
		{
			new BeerCreatedEventHandler(loggerFactory, beerService),
			new BeersReceivedSaga(serviceBus, sagaRepository, loggerFactory)
		};
	}
}