using BrewUp.Warehouse.ApplicationServices.Abstracts;
using Microsoft.AspNetCore.Http;

namespace BrewUp.Warehouse.ApplicationServices.Endpoints;

public static class WarehouseEndpoints
{
    public static async Task<IResult> HandleGetAvailability(IWarehouseOrchestrator warehouseOrchestrator, CancellationToken cancellationToken)
    {
        var availability = await warehouseOrchestrator.GetBeerAvailabilityAsync();
        
        return Results.Ok(availability);
    }
}