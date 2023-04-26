namespace Brewup.Purchases.Rest.Models;

public static class Order
{
	public static Task<IResult> CreateOrder(BindingModels.Order bindingModel)
	{
		//Invariants checks.....


		//TODO: Send message to queue


		return Task.FromResult(Results.Ok());
	}
}