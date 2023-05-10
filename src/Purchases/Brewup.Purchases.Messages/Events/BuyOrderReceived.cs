using Brewup.Purchases.SharedKernel.DomainIds;
using Muflone.Messages.Events;

namespace Brewup.Purchases.Messages.Events;

public class BuyOrderReceived : DomainEvent
{
	public BuyOrderReceived(BuyOrderId aggregateId) : base(aggregateId)
	{
	}
}