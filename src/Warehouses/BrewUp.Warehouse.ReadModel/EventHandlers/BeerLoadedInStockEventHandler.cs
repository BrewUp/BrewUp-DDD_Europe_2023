using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.ReadModel.Services;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.ReadModel.EventHandlers;

public sealed class BeerLoadedInStockEventHandler : DomainEventHandlerBase<BeerLoadedInStock>
{
	private readonly IBeerService _beerService;

	public BeerLoadedInStockEventHandler(ILoggerFactory loggerFactory, IBeerService beerService) : base(loggerFactory)
	{
		_beerService = beerService;
	}

	public override async Task HandleAsync(BeerLoadedInStock @event, CancellationToken cancellationToken = new())
	{
		await _beerService.LoadBeerInStockAsync(@event.BeerId, @event.Stock, cancellationToken);
	}
}