using BrewUp.Warehouse.SharedKernel.DomainIds;

namespace BrewUp.Warehouse.SharedKernel.Dtos;

public class OrderLine
{
	public ProductId ProductId { get; set; } = default!;
	public string Title { get; set; } = string.Empty;
	public Quantity Quantity { get; set; } = default!;
	public Price Price { get; set; } = default!;
}