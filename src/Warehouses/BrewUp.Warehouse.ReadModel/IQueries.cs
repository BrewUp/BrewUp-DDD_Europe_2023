using BrewUp.Warehouse.ReadModel.Entities;
using System.Linq.Expressions;

namespace BrewUp.Warehouse.ReadModel;

public interface IQueries<T> where T : EntityBase
{
	Task<T> GetById(string id);
	Task<PagedResult<T>> GetByFilter(Expression<Func<T, bool>> query, int page, int pageSize);
}