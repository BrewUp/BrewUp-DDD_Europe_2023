using Muflone.Core;

namespace Brewup.Purchases.SharedKernel.DomainIds;

public class OrderId : DomainId
{
	public OrderId(Guid value) : base(value)
	{
	}
}