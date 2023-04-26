using Microsoft.OpenApi.Models;

namespace Brewup.Purchases.Rest.Modules;

public sealed class SwaggerModule : IModule
{
	public bool IsEnabled { get; }
	public int Order { get; }

	public SwaggerModule()
	{
		IsEnabled = true;
		Order = 0;
	}

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
		{
			Description = "BrewUp API - Purchases REST Service",
			Title = "BrewUp Api - Purchases",
			Version = "v1",
			Contact = new OpenApiContact
			{
				Name = "BrewUp.API.Purchases"
			}
		}));

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		return endpoints;
	}
}