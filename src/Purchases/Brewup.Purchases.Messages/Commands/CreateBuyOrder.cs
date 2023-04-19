using Brewup.Purchases.SharedKernel.DomainIds;
using Brewup.Purchases.SharedKernel.DTOs;
using Muflone.Messages.Commands;

namespace Brewup.Purchases.Messages.Commands;

public class CreateBuyOrder : Command
{
	public SupplierId SupplierId { get; }
	public DateTime Date { get; }
	public IEnumerable<OrderLine> Lines { get; }

	public CreateBuyOrder(OrderId aggregateId, SupplierId supplierId, DateTime date, IEnumerable<OrderLine> lines) :
		base(aggregateId)
	{
		SupplierId = supplierId;
		Date = date;
		Lines = lines;
	}
}