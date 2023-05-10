using Muflone.Core;

namespace Brewup.Purchases.SharedKernel.DomainIds;

public class BuyOrderId : DomainId
{
	public BuyOrderId(Guid value) : base(value)
	{
	}
}