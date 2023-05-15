using BrewUp.Warehouse.SharedKernel.Dtos;
using FluentValidation;

namespace BrewUp.Warehouse.ApplicationServices.Validators;

public sealed class OrderLineValidator : AbstractValidator<OrderLineJson>
{
	public OrderLineValidator()
	{
		RuleFor(v => v.Price).SetValidator(new PriceValidator());
		RuleFor(v => v.Quantity).SetValidator(new QuantityValidator());
	}
}