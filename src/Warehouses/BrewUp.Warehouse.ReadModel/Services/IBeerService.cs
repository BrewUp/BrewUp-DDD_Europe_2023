using BrewUp.Warehouse.SharedKernel.DomainIds;

namespace BrewUp.Warehouse.ReadModel.Services;

public interface IBeerService
{
	Task<BeerId> AddBeerAsync(BeerId beerId, BeerName beerName, CancellationToken cancellationToken = default);
	Task LoadBeerInStockAsync(BeerId beerId, Stock stock, CancellationToken cancellationToken = default);
}