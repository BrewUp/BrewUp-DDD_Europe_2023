using Brewup.Purchases.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace Brewup.Purchases.ApplicationService.EventHandlers;

public sealed class BuyOrderCreatedEventHandler : DomainEventHandlerAsync<BuyOrderCreated>
{
	public BuyOrderCreatedEventHandler(ILoggerFactory loggerFactory) : base(loggerFactory)
	{
	}

	public override Task HandleAsync(BuyOrderCreated @event, CancellationToken cancellationToken = new())
	{
		return Task.CompletedTask;
	}
}