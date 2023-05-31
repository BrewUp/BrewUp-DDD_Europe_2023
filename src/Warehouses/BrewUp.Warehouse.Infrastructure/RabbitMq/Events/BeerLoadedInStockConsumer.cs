using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.ReadModel.EventHandlers;
using BrewUp.Warehouse.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouse.Infrastructure.RabbitMq.Events;

public sealed class BeerLoadedInStockConsumer : DomainEventsConsumerBase<BeerLoadedInStock>
{
	protected override IEnumerable<IDomainEventHandlerAsync<BeerLoadedInStock>> HandlersAsync { get; }

	public BeerLoadedInStockConsumer(IBeerService beerService,
		IMufloneConnectionFactory mufloneConnectionFactory,
		RabbitMQReference rabbitMQReference,
		ILoggerFactory loggerFactory) : base(mufloneConnectionFactory, rabbitMQReference, loggerFactory)
	{
		HandlersAsync = new List<IDomainEventHandlerAsync<BeerLoadedInStock>>
		{
			new BeerLoadedInStockEventHandler(loggerFactory, beerService)
		};
	}
}