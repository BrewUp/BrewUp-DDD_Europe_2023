using Brewup.Purchases.SharedKernel.DomainIds;
using Muflone.Messages.Events;

namespace Brewup.Purchases.Messages.Events;

public class PurchaseOrderStatusChangedToComplete : DomainEvent
{
	public PurchaseOrderStatusChangedToComplete(PurchaseOrderId aggregateId) : base(aggregateId)
	{
	}
}