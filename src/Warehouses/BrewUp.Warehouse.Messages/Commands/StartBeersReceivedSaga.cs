using BrewUp.Warehouse.SharedKernel.DomainIds;
using BrewUp.Warehouse.SharedKernel.Dtos;
using Muflone.Messages.Commands;

namespace BrewUp.Warehouse.Messages.Commands;

public sealed class StartBeersReceivedSaga : Command
{
	public readonly PurchaseOrderId BuyOrderId;

	public readonly IEnumerable<OrderLine> OrderLines;

	public StartBeersReceivedSaga(PurchaseOrderId aggregateId, IEnumerable<OrderLine> orderLines)
		: base(aggregateId)
	{
		BuyOrderId = aggregateId;
		OrderLines = orderLines;
	}
}