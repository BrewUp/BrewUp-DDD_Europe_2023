using BrewUp.Warehouses.Messages.Events;
using BrewUp.Warehouses.ReadModel.EventHandlers;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.Sagas.Sagas;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;
using Muflone.Transport.RabbitMQ.Saga.Consumers;

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
			new BeerLoadedInStockEventHandler(loggerFactory, beerService)
			//new BeersReceivedSaga(serviceBus, sagaRepository, loggerFactory)
		};
	}
}

public sealed class BeerLoadedInStockSagaConsumer : SagaEventConsumerBase<BeerCreated>
{
	public BeerLoadedInStockSagaConsumer(
		IServiceBus serviceBus,
		ISagaRepository sagaRepository,
		IMufloneConnectionFactory mufloneConnectionFactory,
		RabbitMQReference rabbitMQReference, ILoggerFactory loggerFactory) : base(mufloneConnectionFactory,
		rabbitMQReference, loggerFactory)
	{
		HandlerAsync = new BeersReceivedSaga(serviceBus, sagaRepository, loggerFactory);
	}

	protected override ISagaEventHandlerAsync<BeerCreated> HandlerAsync { get; }
}