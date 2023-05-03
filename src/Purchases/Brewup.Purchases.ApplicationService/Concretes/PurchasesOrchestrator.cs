using Brewup.Purchases.ApplicationService.Abstracts;
using Brewup.Purchases.ApplicationService.BindingModels;
using Brewup.Purchases.Messages.Commands;
using Brewup.Purchases.SharedKernel.DomainIds;

namespace Brewup.Purchases.ApplicationService.Concretes;

public sealed class PurchasesOrchestrator : IPurchasesOrchestrator
{
	//private readonly IServiceBus _serviceBus;

	//public PurchasesOrchestrator(IServiceBus serviceBus)
	//{
	//	_serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
	//}

	public async Task<string> CreateOrderAsync(Order order, CancellationToken cancellationToken)
	{
		//Application level validation
		var createOrder = new CreateBuyOrder(new OrderId(order.Id),
			new SupplierId(order.SupplierId), order.Date,
			order.Lines.ToDto()); 

		//await _serviceBus.SendAsync(createOrder, cancellationToken);

		return order.Id.ToString();
	}
}