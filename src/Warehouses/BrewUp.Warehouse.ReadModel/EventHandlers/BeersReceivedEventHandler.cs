using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouse.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouse.ReadModel.EventHandlers;

public sealed class BeersReceivedEventHandler : IntegrationEventHandlerBase<BeersReceived>
{
	private readonly IServiceBus _serviceBus;

	public BeersReceivedEventHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus) : base(loggerFactory)
	{
		_serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
	}

	public override async Task HandleAsync(BeersReceived @event, CancellationToken cancellationToken = new())
	{
		var correlationId = new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
		if (correlationId.Equals(Guid.Empty))
			return;

		foreach (var orderLine in @event.OrderLines)
		{
			var createBeer = new CreateBeer(orderLine.BeerId, correlationId, orderLine.BeerName);
			await _serviceBus.SendAsync(createBeer, cancellationToken);
		}
	}
}