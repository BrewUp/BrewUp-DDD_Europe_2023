using BrewUp.Warehouse.ApplicationServices.DTOs;

namespace BrewUp.Warehouse.ApplicationServices.Abstracts;

internal interface IWarehouseAvailabilityService
{
    Task<BeerAvailabilityDTO> GetBeerAvailabilityAsync(CancellationToken cancellationToken);
}