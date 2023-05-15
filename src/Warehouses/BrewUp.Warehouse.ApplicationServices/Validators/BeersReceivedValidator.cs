using BrewUp.Warehouse.SharedKernel.Dtos;
using FluentValidation;

namespace BrewUp.Warehouse.ApplicationServices.Validators;

public class BeersReceivedValidator : AbstractValidator<BeersReceivedJson>
{
	public BeersReceivedValidator()
	{
		RuleFor(v => v.OrderId).NotEmpty().NotNull();

		RuleForEach(v => v.OrderLines).SetValidator(new OrderLineValidator());
	}
}