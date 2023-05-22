using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.ReadModel.Services;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.ReadModel.EventHandlers;

public sealed class BeerCreatedEventHandler : DomainEventHandlerBase<BeerCreated>
{
	private readonly IBeerService _beerService;

	public BeerCreatedEventHandler(ILoggerFactory loggerFactory, IBeerService beerService) : base(loggerFactory)
	{
		_beerService = beerService;
	}

	public override async Task HandleAsync(BeerCreated @event, CancellationToken cancellationToken = new())
	{
		await _beerService.AddBeerAsync(@event.BeerId, @event.BeerName, cancellationToken);
	}
}