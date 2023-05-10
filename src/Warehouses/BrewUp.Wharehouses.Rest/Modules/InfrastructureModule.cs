using BrewUp.Warehouse.ApplicationServices.DTOs;
using BrewUp.Warehouse.Infrastructure;
using BrewUp.Warehouse.Infrastructure.MongoDb;

namespace BrewUp.Wharehouses.Rest.Modules;

public class InfrastructureModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 99;
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddMongoDb(
            builder.Configuration.GetSection("BrewUp:MongoDbSettings").Get<MongoDbSettings>()!);
        
        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }
}