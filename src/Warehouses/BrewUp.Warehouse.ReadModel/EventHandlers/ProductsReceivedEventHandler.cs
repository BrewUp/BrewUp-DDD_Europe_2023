using BrewUp.Warehouse.Messages.Events;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouse.ReadModel.EventHandlers;

public sealed class ProductsReceivedEventHandler : IntegrationEventHandlerBase<ProductsReceived>
{
	public ProductsReceivedEventHandler(ILoggerFactory loggerFactory) : base(loggerFactory)
	{
	}

	public override Task HandleAsync(ProductsReceived @event, CancellationToken cancellationToken = new())
	{
		throw new NotImplementedException();
	}
}