using BrewUp.Warehouse.ReadModel.Entities;

namespace BrewUp.Warehouse.ReadModel.Services;

public interface IWarehouseAvailabilityService
{
	Task<BeerAvailability> GetBeerAvailabilityAsync(CancellationToken cancellationToken);
}