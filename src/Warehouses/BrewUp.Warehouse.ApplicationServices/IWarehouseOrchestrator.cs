using BrewUp.Warehouse.ReadModel.Entities;
using BrewUp.Warehouse.SharedKernel.Dtos;

namespace BrewUp.Warehouse.ApplicationServices;

public interface IWarehouseOrchestrator
{
	Task<PagedResult<BeerJson>> GetBeerAvailabilityAsync(CancellationToken cancellationToken);
}