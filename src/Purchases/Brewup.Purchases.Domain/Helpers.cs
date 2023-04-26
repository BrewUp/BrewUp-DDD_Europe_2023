using Brewup.Purchases.SharedKernel.DTOs;

namespace Brewup.Purchases.Domain;

public static class Helpers
{
	public static IEnumerable<OrderLine> ToDtos(this IEnumerable<Entities.OrderLine> lines)
	{
		return lines.Select(x => new OrderLine
		{
			ProductId = x.ProductId,
			Price = new Price { Currency = x.Price.Currency, Value = x.Price.Value },
			Quantity = new Quantity { UnitOfMeasure = x.Quantity.UnitOfMeasure, Value = x.Quantity.Value },
			Title = x.Title
		}); //.ToList();
	}

	public static IEnumerable<Entities.OrderLine> ToEntities(this IEnumerable<OrderLine> lines)
	{
		return lines.Select(x => new Entities.OrderLine(
			x.ProductId,
			x.Title,
			new Entities.Quantity(x.Quantity.Value, x.Quantity.UnitOfMeasure),
			new Entities.Price(x.Price.Value, x.Price.Currency))); //.ToList();
	}
}