using BrewUp.Warehouse.SharedKernel.DomainIds;
using BrewUp.Warehouse.SharedKernel.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Warehouse.Messages.Events;

public sealed class BeersReceived : IntegrationEvent
{
	public readonly BuyOrderId BuyOrderId;
	public readonly IEnumerable<OrderLine> OrderLines;

	public BeersReceived(BuyOrderId aggregateId, Guid correlationId, IEnumerable<OrderLine> orderLines) : base(aggregateId, correlationId)
	{
		BuyOrderId = aggregateId;
		OrderLines = orderLines;
	}
}