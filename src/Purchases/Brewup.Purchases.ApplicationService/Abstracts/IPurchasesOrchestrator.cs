namespace Brewup.Purchases.ApplicationService.Abstracts;

public interface IPurchasesOrchestrator
{
	Task<string> CreateOrderAsync(BindingModels.Order order, CancellationToken cancellationToken);
}