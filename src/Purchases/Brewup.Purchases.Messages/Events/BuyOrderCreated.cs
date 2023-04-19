using Brewup.Purchases.SharedKernel.DomainIds;
using Brewup.Purchases.SharedKernel.DTOs;
using Muflone.Messages.Events;

namespace Brewup.Purchases.Messages.Events;

public class BuyOrderCreated : DomainEvent
{
	public SupplierId SupplierId { get; }
	public DateTime Date { get; }
	public IEnumerable<OrderLine> Lines { get; }

	public BuyOrderCreated(OrderId aggregateId, SupplierId supplierId, DateTime date, IEnumerable<OrderLine> lines) :
		base(aggregateId)
	{
		SupplierId = supplierId;
		Date = date;
		Lines = lines;
	}
}