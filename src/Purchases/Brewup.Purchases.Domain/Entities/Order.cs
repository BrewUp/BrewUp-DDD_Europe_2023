using Brewup.Purchases.Messages.Events;
using Brewup.Purchases.SharedKernel.DomainIds;
using Muflone.Core;

namespace Brewup.Purchases.Domain.Entities;

public class Order : AggregateRoot
{
	private SupplierId _supplierId;
	private DateTime _date;
	private IEnumerable<OrderLine> _lines;


	public static Order Create(OrderId id, SupplierId supplierId, DateTime date, IEnumerable<OrderLine> lines)
	{
		return new Order(id, supplierId, date, lines);
	}

	// This is the constructor that is called when the aggregate is loaded from the event store
	protected Order()
	{
	}

	private Order(OrderId id, SupplierId supplierId, DateTime date, IEnumerable<OrderLine> lines)
	{
		//Invariants checks


		/////////

		RaiseEvent(new BuyOrderCreated(id, supplierId, date, lines.ToDtos()));
	}

	private void Apply(BuyOrderCreated @event)
	{
		Id = @event.AggregateId;
		_supplierId = @event.SupplierId;
		_date = @event.Date;
		_lines = @event.Lines.ToEntities();
	}
}