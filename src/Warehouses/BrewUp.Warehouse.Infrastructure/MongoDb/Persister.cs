using BrewUp.Warehouse.ReadModel;
using BrewUp.Warehouse.ReadModel.DTOs;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BrewUp.Warehouse.Infrastructure.MongoDb;

public class Persister : IPersister
{
    private readonly IMongoDatabase _database;
    private readonly ILogger _logger;

    public Persister(IMongoDatabase database, ILoggerFactory loggerFactory)
    {
        _database = database;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task<T> GetBy<T>(string id) where T : ModelBase
    {
        var type = typeof(T).Name;
        try
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq("_id", id);
            return (await collection.CountDocumentsAsync(filter) > 0
                ? (await collection.FindAsync(filter)).First()
                : null)!;
        }
        catch (Exception e)
        {
            _logger.LogError($"Insert: Error saving DTO: {type}, Message: {e.Message}, StackTrace: {e.StackTrace}");
            throw;
        }
    }

    public async Task Insert<T>(T entity) where T : ModelBase
    {
        var type = typeof(T).Name;
        try
        {
            var collection = _database.GetCollection<T>(type);
            await collection.InsertOneAsync(entity);
        }
        catch (Exception e)
        {
            _logger.LogError($"Insert: Error saving DTO: {type}, Message: {e.Message}, StackTrace: {e.StackTrace}");
            throw;
        }
    }

    public async Task Update<T>(T entity) where T : ModelBase
    {
        var type = typeof(T).Name;
        try
        {
            var collection = _database.GetCollection<T>(type);
            await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
        catch (Exception e)
        {
            _logger.LogError($"Update: Error saving DTO: {type}, Message: {e.Message}, StackTrace: {e.StackTrace}");
            throw;
        }
    }

    public async Task Delete<T>(T entity) where T : ModelBase
    {
        var type = typeof(T).Name;
        try
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq("_id", entity.Id);
            await collection.FindOneAndDeleteAsync(filter);
        }
        catch (Exception e)
        {
            _logger.LogError($"Delete: Error saving DTO: {type}, Message: {e.Message}, StackTrace: {e.StackTrace}");
            throw;
        }
    }
}