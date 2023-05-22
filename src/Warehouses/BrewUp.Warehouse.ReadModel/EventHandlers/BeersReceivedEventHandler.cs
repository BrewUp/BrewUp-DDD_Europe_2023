using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.ReadModel.Services;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.ReadModel.EventHandlers;

public sealed class BeersReceivedEventHandler : IntegrationEventHandlerBase<BeersReceived>
{
	private readonly IBeerService _beerService;

	public BeersReceivedEventHandler(ILoggerFactory loggerFactory, IBeerService beerService) : base(loggerFactory)
	{
		_beerService = beerService;
	}

	public override Task HandleAsync(BeersReceived @event, CancellationToken cancellationToken = default)
	{
		//TODO Implement saving beers to read model
		return Task.CompletedTask;
	}
}