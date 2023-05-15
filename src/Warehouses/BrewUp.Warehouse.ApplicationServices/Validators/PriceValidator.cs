using BrewUp.Warehouse.SharedKernel.Dtos;
using FluentValidation;

namespace BrewUp.Warehouse.ApplicationServices.Validators;

public sealed class PriceValidator : AbstractValidator<Price>
{
	public PriceValidator()
	{
		RuleFor(v => v.Currency).NotEmpty().NotNull();
		RuleFor(v => v.Value).GreaterThan(0);
	}
}