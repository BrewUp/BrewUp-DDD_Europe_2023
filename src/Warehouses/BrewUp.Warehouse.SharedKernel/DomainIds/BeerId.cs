using Muflone.Core;

namespace BrewUp.Warehouse.SharedKernel.DomainIds;

public sealed class BeerId : DomainId
{
	public BeerId(Guid value) : base(value)
	{
	}
}