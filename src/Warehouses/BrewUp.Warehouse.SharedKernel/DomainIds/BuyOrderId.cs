using Muflone.Core;

namespace BrewUp.Warehouse.SharedKernel.DomainIds;

public sealed class BuyOrderId : DomainId
{
	public BuyOrderId(Guid value) : base(value)
	{
	}
}