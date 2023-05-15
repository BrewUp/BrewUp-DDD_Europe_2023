using BrewUp.Warehouse.ReadModel.Entities;
using BrewUp.Warehouse.ReadModel.Services;
using BrewUp.Warehouse.SharedKernel.Dtos;

namespace BrewUp.Warehouse.ApplicationServices;

internal class WarehouseOrchestrator : IWarehouseOrchestrator
{
	private readonly IWarehouseAvailabilityService _warehouseAvailabilityService;

	public WarehouseOrchestrator(IWarehouseAvailabilityService warehouseAvailabilityService)
	{
		_warehouseAvailabilityService = warehouseAvailabilityService;
	}

	public Task<PagedResult<BeerJson>> GetBeerAvailabilityAsync(CancellationToken cancellationToken) =>
		_warehouseAvailabilityService.GetBeerAvailabilityAsync(default);
}