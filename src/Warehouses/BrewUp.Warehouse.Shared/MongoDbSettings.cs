namespace BrewUp.Warehouse.ApplicationServices.DTOs;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}