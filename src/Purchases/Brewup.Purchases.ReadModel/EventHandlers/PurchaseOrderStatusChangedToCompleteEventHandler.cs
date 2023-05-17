using Brewup.Purchases.Messages.Events;
using Brewup.Purchases.ReadModel.Services;
using Brewup.Purchases.SharedKernel.DomainIds;
using Microsoft.Extensions.Logging;

namespace Brewup.Purchases.ReadModel.EventHandlers;

public sealed class PurchaseOrderStatusChangedToCompleteEventHandler : DomainEventHandlerBase<PurchaseOrderStatusChangedToComplete>
{
	private readonly IPurchaseOrderService _purchaseOrderService;

	public PurchaseOrderStatusChangedToCompleteEventHandler(ILoggerFactory loggerFactory, IPurchaseOrderService purchaseOrderService) :
		base(loggerFactory)
	{
		_purchaseOrderService = purchaseOrderService;
	}

	public override async Task HandleAsync(PurchaseOrderStatusChangedToComplete @event, CancellationToken cancellationToken = default)
	{
		await _purchaseOrderService.UpdateStatusToComplete(new PurchaseOrderId(@event.AggregateId.Value));
	}
}