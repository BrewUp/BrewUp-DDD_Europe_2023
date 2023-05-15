using BrewUp.Warehouse.Infrastructure.MongoDb.Readmodel;
using BrewUp.Warehouse.ReadModel;
using BrewUp.Warehouse.ReadModel.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Muflone.Eventstore.Persistence;

namespace BrewUp.Warehouse.Infrastructure.MongoDb
{
	public static class MongoDbHelper
	{
		public static IServiceCollection AddMongoDb(this IServiceCollection services,
			MongoDbSettings mongoDbSettings)
		{
			services.AddSingleton<IMongoDatabase>(x =>
			{
				var client = new MongoClient(mongoDbSettings.ConnectionString);
				var database = client.GetDatabase(mongoDbSettings.DatabaseName);
				return database;
			});
			services.AddScoped<IPersister, Persister>();
			services.AddScoped<IQueries<Beer>, BeersQueries>();

			services.AddSingleton<IEventStorePositionRepository>(x =>
				new EventStorePositionRepository(x.GetService<ILogger<EventStorePositionRepository>>(), mongoDbSettings));

			return services;
		}
	}
}
