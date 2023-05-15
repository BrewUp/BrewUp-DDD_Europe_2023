using BrewUp.Warehouse.ApplicationServices.Endpoints;

namespace BrewUp.Warehouses.Rest.Modules;

public sealed class ExternalSystemModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;
	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		var mapGroup = endpoints.MapGroup("/api/v1/")
			.WithTags("ExternalSystems");

		mapGroup.MapPut("/beers", ExternalSystemsEndpoints.HandleUpdateAvailability)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status200OK)
			.WithName("UpdateAvailability");

		return endpoints;
	}
}