using Brewup.Purchases.SharedKernel.DomainIds;

namespace Brewup.Purchases.SharedKernel.DTOs;

public class OrderLine
{
	public ProductId ProductId { get; set; } = default!;
	public string Title { get; set; } = string.Empty;
	public Quantity Quantity { get; set; } = default!;
	public Price Price { get; set; } = default!;
}