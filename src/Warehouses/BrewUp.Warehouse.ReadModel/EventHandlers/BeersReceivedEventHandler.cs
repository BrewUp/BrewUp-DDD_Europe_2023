﻿using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouse.Messages.Events;
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
			var command = new CreateBeer(orderLine.BeerId, @event.MessageId, orderLine.BeerName);
			await _serviceBus.SendAsync(command, cancellationToken);
		}
	}
}