using Muflone.Core;
using Muflone.Messages.Events;

namespace BrewUp.Warehouse.Messages.Events;

public sealed class ProductsReceived : IntegrationEvent
{
	public ProductsReceived(IDomainId aggregateId, Guid correlationId) : base(aggregateId, correlationId)
	{
	}
}