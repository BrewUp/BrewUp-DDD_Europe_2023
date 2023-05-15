using BrewUp.Warehouse.SharedKernel.Dtos;
using FluentValidation;

namespace BrewUp.Warehouse.ApplicationServices.Validators;

public sealed class QuantityValidator : AbstractValidator<Quantity>
{
	public QuantityValidator()
	{
		RuleFor(v => v.UnitOfMeasure).NotNull().NotEmpty();
		RuleFor(v => v.Value).GreaterThan(0);
	}
}