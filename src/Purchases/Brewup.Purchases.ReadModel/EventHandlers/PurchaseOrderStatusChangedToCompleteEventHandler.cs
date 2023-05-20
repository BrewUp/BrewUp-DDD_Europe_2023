using Brewup.Purchases.Messages.Events;
using Brewup.Purchases.ReadModel.Services;
using Brewup.Purchases.SharedKernel.DomainIds;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;

namespace Brewup.Purchases.ReadModel.EventHandlers;

public sealed class
	PurchaseOrderStatusChangedToCompleteEventHandler : DomainEventHandlerBase<PurchaseOrderStatusChangedToComplete>
{
	private readonly IEventBus _eventBus;
	private readonly IPurchaseOrderService _purchaseOrderService;

	public PurchaseOrderStatusChangedToCompleteEventHandler(IEventBus eventBus, ILoggerFactory loggerFactory,
		IPurchaseOrderService purchaseOrderService) :
		base(loggerFactory)
	{
		_eventBus = eventBus;
		_purchaseOrderService = purchaseOrderService;
	}

	public override async Task HandleAsync(PurchaseOrderStatusChangedToComplete @event,
		CancellationToken cancellationToken = default)
	{
		await _purchaseOrderService.UpdateStatusToComplete(new PurchaseOrderId(@event.AggregateId.Value));

		//[IntegrationEvent] We are lazy, it would be better to create a dedicated EventHandler 
		await _eventBus.PublishAsync(
			new BeersReceived(new PurchaseOrderId(@event.AggregateId.Value), Guid.NewGuid(), @event.Lines),
			cancellationToken);
	}
}