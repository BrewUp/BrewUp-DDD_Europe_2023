namespace Brewup.Purchases.ApplicationService.BindingModels;

public static class Helpers
{
	public static SharedKernel.DTOs.OrderLine ToDto(this OrderLine orderLine)
	{
		return new SharedKernel.DTOs.OrderLine
		{
			ProductId = new SharedKernel.DomainIds.ProductId(orderLine.ProductId),
			Title = orderLine.Title,
			Quantity = new SharedKernel.DTOs.Quantity
			{
				Value = orderLine.Quantity.Value,
				UnitOfMeasure = orderLine.Quantity.UnitOfMeasure
			},
			Price = new SharedKernel.DTOs.Price
			{
				Value = orderLine.Price.Value,
				Currency = orderLine.Price.Currency
			}
		};
	}
}