using BrewUp.Warehouse.ReadModel.Entities;
using BrewUp.Warehouse.ReadModel.Services;

namespace BrewUp.Warehouse.ApplicationServices;

internal class WarehouseOrchestrator : IWarehouseOrchestrator
{
	private readonly IWarehouseAvailabilityService _warehouseAvailabilityService;

	public WarehouseOrchestrator(IWarehouseAvailabilityService warehouseAvailabilityService)
	{
		_warehouseAvailabilityService = warehouseAvailabilityService;
	}

	public Task<BeerAvailability> GetBeerAvailabilityAsync(CancellationToken cancellationToken)
	{
		return _warehouseAvailabilityService.GetBeerAvailabilityAsync(default);
	}
}