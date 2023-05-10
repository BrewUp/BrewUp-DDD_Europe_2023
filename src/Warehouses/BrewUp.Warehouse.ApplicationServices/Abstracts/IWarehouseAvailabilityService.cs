using BrewUp.Warehouse.ApplicationServices.DTOs;

namespace BrewUp.Warehouse.ApplicationServices.Abstracts;

internal interface IWarehouseAvailabilityService
{
    Task<BeerAvailabilityModelBase> GetBeerAvailabilityAsync(CancellationToken cancellationToken);
}