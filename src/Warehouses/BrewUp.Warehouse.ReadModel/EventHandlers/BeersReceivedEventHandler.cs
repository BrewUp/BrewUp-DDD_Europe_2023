using BrewUp.Warehouse.Messages.Events;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.ReadModel.EventHandlers;

public sealed class BeersReceivedEventHandler : IntegrationEventHandlerBase<BeersReceived>
{
	public BeersReceivedEventHandler(ILoggerFactory loggerFactory) : base(loggerFactory)
	{
	}

	public override Task HandleAsync(BeersReceived @event, CancellationToken cancellationToken = new())
	{
		throw new NotImplementedException();
	}
}