using BrewUp.Warehouse.SharedKernel.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Warehouse.Messages.Events;

public sealed class BeerCreated : DomainEvent
{
	public readonly BeerId BeerId;
	public readonly BeerName BeerName;

	public BeerCreated(BeerId aggregateId, Guid correlationId, BeerName beerName) : base(aggregateId, correlationId)
	{
		BeerId = aggregateId;
		BeerName = beerName;
	}
}