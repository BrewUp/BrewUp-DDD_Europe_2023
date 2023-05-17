using Brewup.Purchases.Messages.Events;
using Brewup.Purchases.SharedKernel.DomainIds;
using Brewup.Purchases.SharedKernel.DTOs;
using Muflone.Core;

namespace Brewup.Purchases.Domain.Entities;

public class Order : AggregateRoot
{
	private SupplierId _supplierId;
	private DateTime _date;
	private IEnumerable<OrderLine> _lines;
	private OrderStatus _status;

	//Called when loaded from the event store
	protected Order()
	{
	}

	public static Order Create(PurchaseOrderId id, SupplierId supplierId, DateTime date,
		IEnumerable<SharedKernel.DTOs.OrderLine> lines)
	{
		return new Order(id, supplierId, date, lines);
	}


	private Order(PurchaseOrderId id, SupplierId supplierId, DateTime date,
		IEnumerable<SharedKernel.DTOs.OrderLine> lines)
	{
		//Invariants checks
		//if (!_lines.Any())
		//	throw new ArgumentException("Order must have at least one line", nameof(lines));

		/////////
		RaiseEvent(new PurchaseOrderCreated(id, supplierId, date, lines));
	}

	private void Apply(PurchaseOrderCreated @event)
	{
		Id = @event.AggregateId;
		_status = OrderStatus.Created;
		//_supplierId = @event.SupplierId;
		//_date = @event.Date;
		//_lines = @event.Lines.ToEntities();
	}

	public void Complete()
	{
		if (!_status.Equals(OrderStatus.Complete))
			RaiseEvent(new PurchaseOrderStatusChangedToComplete((PurchaseOrderId)Id));
	}

	private void Apply(PurchaseOrderStatusChangedToComplete @event)
	{
		_status = OrderStatus.Complete;
	}
}