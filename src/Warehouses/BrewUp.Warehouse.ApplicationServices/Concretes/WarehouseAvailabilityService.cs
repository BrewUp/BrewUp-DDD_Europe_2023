using BrewUp.Warehouse.ApplicationServices.Abstracts;
using BrewUp.Warehouse.ApplicationServices.DTOs;

namespace BrewUp.Warehouse.ApplicationServices.Concretes;

internal class WarehouseAvailabilityService : IWarehouseAvailabilityService
{
    public Task<BeerAvailabilityDTO> GetBeerAvailabilityAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new BeerAvailabilityDTO
        {
            BeerId = Guid.NewGuid().ToString(),
            BeerName = "Muflone IPA",
            Availability = "100l"
        });
    }
}