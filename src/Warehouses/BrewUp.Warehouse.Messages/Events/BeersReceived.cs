using BrewUp.Warehouse.SharedKernel.DomainIds;
using BrewUp.Warehouse.SharedKernel.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Warehouse.Messages.Events;

public sealed class BeersReceived : IntegrationEvent
{
	public readonly BeerId BeerId;
	public readonly BeerName BeerName;
	public readonly Quantity Quantity;

	public BeersReceived(BeerId aggregateId, Guid correlationId, BeerName beerName, Quantity quantity) : base(aggregateId, correlationId)
	{
		BeerId = aggregateId;
		BeerName = beerName;
		Quantity = quantity;
	}
}