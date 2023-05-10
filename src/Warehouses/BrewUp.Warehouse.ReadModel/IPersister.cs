using BrewUp.Warehouse.ReadModel.DTOs;

namespace BrewUp.Warehouse.ReadModel;

public interface IPersister
{
    Task<T> GetBy<T>(string id) where T : ModelBase;
    Task Insert<T>(T entity) where T : ModelBase;
    Task Update<T>(T entity) where T : ModelBase;
    Task Delete<T>(T entity) where T : ModelBase;
}