using Muflone.Core;

namespace BrewUp.Warehouse.SharedKernel.DomainIds;

public sealed class PurchaseOrderId : DomainId
{
	public PurchaseOrderId(Guid value) : base(value)
	{
	}
}