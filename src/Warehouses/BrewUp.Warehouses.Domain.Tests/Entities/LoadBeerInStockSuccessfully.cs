using BrewUp.Warehouse.Messages.Commands;
using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.SharedKernel.DomainIds;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace BrewUp.Warehouses.Domain.Tests.Entities;

public sealed class LoadBeerInStockSuccessfully : CommandSpecification<LoadBeerInStock>
{
	public readonly BeerId _beerId = new(Guid.NewGuid());
	public readonly BeerName _beerName = new("Muflone IPA");
	public readonly Stock _stock = new(10);

	public readonly Guid _correlationId = Guid.NewGuid();

	protected override IEnumerable<DomainEvent> Given()
	{
		yield return new BeerCreated(_beerId, _correlationId, _beerName);
	}

	protected override LoadBeerInStock When()
	{
		throw new NotImplementedException();
	}

	protected override ICommandHandlerAsync<LoadBeerInStock> OnHandler()
	{
		throw new NotImplementedException();
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		throw new NotImplementedException();
	}
}