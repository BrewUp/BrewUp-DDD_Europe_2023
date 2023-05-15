using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.SharedKernel.DomainIds;
using Muflone.Core;

namespace BrewUp.Warehouses.Domain.Entities;

public sealed class Beer : AggregateRoot
{
	private BeerName _beerName;

	protected Beer()
	{ }

	internal static Beer CreateBeer(BeerId beerId, BeerName beerName, Guid correlationId)
	{
		return new Beer(beerId, beerName, correlationId);
	}

	private Beer(BeerId beerId, BeerName beerName, Guid correlationId)
	{
		RaiseEvent(new BeerCreated(beerId, correlationId, beerName));
	}

	private void Apply(BeerCreated @event)
	{
		Id = @event.BeerId;
		_beerName = @event.BeerName;
	}
}