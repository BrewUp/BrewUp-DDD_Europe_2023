using Brewup.Purchases.Infrastructure.MongoDb.Readmodel;
using Brewup.Purchases.SharedKernel.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Muflone.Eventstore.Persistence;

namespace Brewup.Purchases.Infrastructure.MongoDb
{
  public static class MongoDBHelpers
  {
    public static IServiceCollection AddMongoDb(this IServiceCollection services, string connectionString)
    {
      services.AddSingleton<IMongoDatabase>(x =>
      {
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

        //BsonClassMap.RegisterClassMap<Enumeration>(cm =>
        //{
        //  cm.SetIsRootClass(true);
        //  cm.MapMember(m => m.Id);
        //  cm.MapMember(m => m.Name);
        //});
        //BsonClassMap.RegisterClassMap<SeatState>(cm =>
        //{
        //  cm.MapCreator(c => new SeatState(c.Id, c.Name));
        //});
        
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("Purchases_ReadModel"); //Best to inject a class with all the parameters and not being coupled like this
        return database;
      });
      services.AddScoped<IPersister, Persister>();
      services.AddSingleton<IEventStorePositionRepository>(x => new EventStorePositionRepository(x.GetService<ILogger<EventStorePositionRepository>>(), connectionString));
      return services;
    }
  }
}
