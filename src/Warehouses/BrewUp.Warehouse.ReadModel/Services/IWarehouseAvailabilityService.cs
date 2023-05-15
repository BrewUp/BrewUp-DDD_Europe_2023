using BrewUp.Warehouse.ReadModel.Entities;
using BrewUp.Warehouse.SharedKernel.Dtos;

namespace BrewUp.Warehouse.ReadModel.Services;

public interface IWarehouseAvailabilityService
{
	Task<PagedResult<BeerJson>> GetBeerAvailabilityAsync(CancellationToken cancellationToken);
}