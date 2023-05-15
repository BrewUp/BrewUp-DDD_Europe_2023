using BrewUp.Warehouse.ApplicationServices.Validators;
using BrewUp.Warehouse.Messages.Events;
using BrewUp.Warehouse.SharedKernel;
using BrewUp.Warehouse.SharedKernel.DomainIds;
using BrewUp.Warehouse.SharedKernel.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Muflone;

namespace BrewUp.Warehouse.ApplicationServices.Endpoints;

public static class ExternalSystemsEndpoints
{
	public static async Task<IResult> HandleUpdateAvailability(
		IValidator<BeersReceivedJson> validator,
		ValidationHandler validationHandler,
		BeersReceivedJson body,
		IEventBus eventBus,
		CancellationToken cancellationToken)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		var beersReceived = new BeersReceived(new BuyOrderId(new Guid(body.OrderId)),
			Guid.NewGuid(), body.OrderLines.ToEntity());

		await eventBus.PublishAsync(beersReceived, cancellationToken);

		return Results.Ok();
	}
}