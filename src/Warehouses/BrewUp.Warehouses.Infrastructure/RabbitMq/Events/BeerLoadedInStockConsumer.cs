using BrewUp.Warehouses.Messages.Events;
using BrewUp.Warehouses.ReadModel.EventHandlers;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.Sagas.Sagas;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouses.Infrastructure.RabbitMq.Events;

public sealed class BeerLoadedInStockConsumer : DomainEventsConsumerBase<BeerLoadedInStock>
{
	protected override IEnumerable<IDomainEventHandlerAsync<BeerLoadedInStock>> HandlersAsync { get; }

	public BeerLoadedInStockConsumer(IServiceBus serviceBus,
		ISagaRepository sagaRepository,
		IBeerService beerService,
		IMufloneConnectionFactory mufloneConnectionFactory,
		RabbitMQReference rabbitMQReference,
		ILoggerFactory loggerFactory) : base(mufloneConnectionFactory, rabbitMQReference, loggerFactory)
	{
		HandlersAsync = new List<IDomainEventHandlerAsync<BeerLoadedInStock>>
		{
			new BeerLoadedInStockEventHandler(loggerFactory, beerService),
			new BeersReceivedSaga(serviceBus, sagaRepository, beerService, loggerFactory)
		};
	}
}