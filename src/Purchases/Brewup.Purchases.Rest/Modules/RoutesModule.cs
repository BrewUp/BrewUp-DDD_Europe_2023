namespace Brewup.Purchases.Rest.Modules;

public class RoutesModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/api/v1/");
		group.MapGet("/", () => Results.Ok());
		group.MapPost("/Order", Models.Order.CreateOrder);

		return endpoints;
	}
}