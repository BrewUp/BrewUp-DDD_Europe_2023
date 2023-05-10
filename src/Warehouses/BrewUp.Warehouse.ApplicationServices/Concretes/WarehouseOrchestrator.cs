using BrewUp.Warehouse.ApplicationServices.Abstracts;
using BrewUp.Warehouse.ApplicationServices.DTOs;

namespace BrewUp.Warehouse.ApplicationServices.Concretes;

internal class WarehouseOrchestrator : IWarehouseOrchestrator
{
    private readonly IWarehouseAvailabilityService _warehouseAvailabilityService;

    public WarehouseOrchestrator(IWarehouseAvailabilityService warehouseAvailabilityService)
    {
        _warehouseAvailabilityService = warehouseAvailabilityService;
    }

    public Task<BeerAvailabilityModelBase> GetBeerAvailabilityAsync(CancellationToken cancellationToken)
    {
        return _warehouseAvailabilityService.GetBeerAvailabilityAsync(default);
    }
}