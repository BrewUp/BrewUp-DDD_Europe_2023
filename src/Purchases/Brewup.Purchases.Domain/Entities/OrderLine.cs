using Brewup.Purchases.SharedKernel.DomainIds;
using Muflone.Core;

namespace Brewup.Purchases.Domain.Entities;

public class OrderLine : Entity
{
	public ProductId ProductId { get; }
	public string Title { get; }
	public Quantity Quantity { get; }
	public Price Price { get; }

	public OrderLine(ProductId productId, string title, Quantity quantity, Price price)
	{
		ProductId = productId;
		Title = title;
		Quantity = quantity;
		Price = price;
	}
}