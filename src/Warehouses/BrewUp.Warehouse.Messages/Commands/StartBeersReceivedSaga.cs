using BrewUp.Warehouse.SharedKernel.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUp.Warehouse.Messages.Commands;

public sealed class StartBeersReceivedSaga : Command
{
	public readonly BeerId BeerId;
	public readonly BeerName BeerName;

	public StartBeersReceivedSaga(BeerId aggregateId, Guid commitId, BeerName beerName)
		: base(aggregateId, commitId)
	{
		BeerId = aggregateId;
		BeerName = beerName;
	}
}