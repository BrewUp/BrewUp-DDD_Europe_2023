using BrewUp.Warehouses.SharedKernel.DomainIds;
using BrewUp.Warehouses.SharedKernel.Dtos;
using Muflone.Core;

namespace BrewUp.Warehouses.Domain.Entities;

public sealed class Beer : AggregateRoot
{
	//private BeerName _beerName;

	private IEnumerable<StockMovement> _movements;

	private Stock _stock;
	private Price _price;

	internal Beer()
	{
	}

	internal static Beer CreateBeer(BeerId beerId, BeerName beerName, Guid correlationId)
	{
		return new Beer(beerId, beerName, correlationId);
	}

	private Beer(BeerId beerId, BeerName beerName, Guid correlationId)
	{
		// Raise DomainEvent(new BeerCreated(beerId, beerName), correlationId);
	}
}