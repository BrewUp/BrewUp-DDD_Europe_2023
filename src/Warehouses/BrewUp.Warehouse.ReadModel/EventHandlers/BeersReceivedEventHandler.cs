using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.SharedKernel.DomainIds;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouse.ReadModel.EventHandlers;

public sealed class BeersReceivedEventHandler : IntegrationEventHandlerBase<BeersReceived>
{
	private readonly IServiceBus _serviceBus;

	public BeersReceivedEventHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus) : base(loggerFactory)
	{
		_serviceBus = serviceBus;
	}

	public override async Task HandleAsync(BeersReceived @event, CancellationToken cancellationToken = default)
	{
		foreach (var orderLine in @event.OrderLines)
		{
			var createBeer = new CreateBeer(orderLine.BeerId, @event.MessageId, orderLine.BeerName);
			await _serviceBus.SendAsync(createBeer, cancellationToken);

			// We know. a Saga would be better! ... but we are lazy :-)
			Thread.Sleep(5000);

			var loadBeerInStock = new LoadBeerInStock(orderLine.BeerId, new Stock((double)orderLine.Quantity.Value), @event.PurchaseOrderId);
			await _serviceBus.SendAsync(loadBeerInStock, cancellationToken);
		}
	}
}