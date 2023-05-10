using BrewUp.Warehouse.ReadModel.Entities;

namespace BrewUp.Warehouse.ApplicationServices;

public interface IWarehouseOrchestrator
{
	Task<BeerAvailability> GetBeerAvailabilityAsync(CancellationToken cancellationToken);
}