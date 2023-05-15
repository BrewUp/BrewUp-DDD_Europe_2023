using BrewUp.Warehouse.Infrastructure.MongoDb;
using BrewUp.Warehouse.Infrastructure.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;
using Muflone.Saga.Persistence.MongoDb;

namespace BrewUp.Warehouse.Infrastructure;

public static class InfrastructureHelper
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		EventStoreSettings eventStoreSettings,
		RabbitMqSettings rabbitMqSettings)
	{
		services.AddMongoDb(mongoDbSettings);
		services.AddMongoSagaStateRepository(new MongoSagaStateRepositoryOptions(mongoDbSettings.ConnectionString, mongoDbSettings.DatabaseName));
		services.AddMufloneEventStore(eventStoreSettings.ConnectionString);
		services.AddRabbitMq(rabbitMqSettings);

		return services;
	}
}