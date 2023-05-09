using Microsoft.AspNetCore.Http;

namespace BrewUp.Warehouse.ApplicationServices.Endpoints;

public static class WarehouseEndpoints
{
    public static Task<IResult> HandleGetAvailability()
    {

        return Task.FromResult(Results.Ok());
    }
}