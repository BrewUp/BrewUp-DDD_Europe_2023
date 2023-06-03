using BrewUp.Warehouses.Messages.Events;
using BrewUp.Warehouses.ReadModel.EventHandlers;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.Sagas;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Saga.Consumers;

namespace BrewUp.Warehouses.Infrastructure.RabbitMq.Events;

public sealed class BeerCreatedConsumer : DomainEventsConsumerBase<BeerCreated>
{
	protected override IEnumerable<IDomainEventHandlerAsync<BeerCreated>> HandlersAsync { get; }

	public BeerCreatedConsumer(IBeerService beerService, IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory) 
		: base(connectionFactory, loggerFactory)
	{
		HandlersAsync = new List<IDomainEventHandlerAsync<BeerCreated>>
		{
			new BeerCreatedEventHandler(loggerFactory, beerService)
		};
	}
}

public sealed class BeerCreatedSagaConsumer : SagaEventConsumerBase<BeerCreated>
{
	public BeerCreatedSagaConsumer(IServiceBus serviceBus, ISagaRepository sagaRepository, IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory) 
		: base(connectionFactory,loggerFactory)
	{
		HandlerAsync = new BeersReceivedSaga(serviceBus, sagaRepository, loggerFactory);
	}

	protected override ISagaEventHandlerAsync<BeerCreated> HandlerAsync { get; } 
}