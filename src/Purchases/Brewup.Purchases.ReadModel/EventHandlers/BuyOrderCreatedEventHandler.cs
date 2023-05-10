using Brewup.Purchases.Messages.Events;
using Brewup.Purchases.ReadModel.Services;
using Brewup.Purchases.SharedKernel.DomainIds;
using Microsoft.Extensions.Logging;

namespace Brewup.Purchases.ReadModel.EventHandlers;

public sealed class BuyOrderCreatedEventHandler : DomainEventHandlerBase<BuyOrderCreated>
{
	private readonly IBuyOrderService _buyOrderService;

	public BuyOrderCreatedEventHandler(ILoggerFactory loggerFactory, IBuyOrderService buyOrderService) :
		base(loggerFactory)
	{
		_buyOrderService = buyOrderService;
	}

	public override async Task HandleAsync(BuyOrderCreated @event, CancellationToken cancellationToken = new())
	{
		await _buyOrderService.CreateBuyOrder(new BuyOrderId(@event.AggregateId.Value), @event.Date, @event.Lines, @event.SupplierId);
	}
}