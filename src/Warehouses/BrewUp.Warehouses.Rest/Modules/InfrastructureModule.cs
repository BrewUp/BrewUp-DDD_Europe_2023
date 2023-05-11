using BrewUp.Warehouse.Infrastructure.MongoDb;
using BrewUp.Warehouse.Infrastructure.RabbitMq;
using Muflone.Eventstore;

namespace BrewUp.Warehouses.Rest.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 99;
	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddMongoDb(
			builder.Configuration.GetSection("BrewUp:MongoDb").Get<MongoDbSettings>()!);
		builder.Services.AddMufloneEventStore(builder.Configuration["BrewUp:EventStore:ConnectionString"]!);
		builder.Services.AddRabbitMq(builder.Configuration.GetSection("BrewUp:RabbitMQ").Get<RabbitMqSettings>()!);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		return endpoints;
	}
}