namespace Brewup.Purchases.ApplicationService;

public interface IPurchasesOrchestrator
{
    Task<string> CreateOrderAsync(BindingModels.Order order, CancellationToken cancellationToken);
}