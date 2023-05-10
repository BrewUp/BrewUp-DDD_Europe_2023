using BrewUp.Warehouse.ReadModel;
using BrewUp.Warehouse.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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
            
            return services;
        }
    }
}
