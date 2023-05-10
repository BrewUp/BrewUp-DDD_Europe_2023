using Brewup.Purchases.SharedKernel.DomainIds;
using Brewup.Purchases.SharedKernel.DTOs;

namespace Brewup.Purchases.ReadModel.Services;

public interface IBuyOrderService
{
	Task CreateBuyOrder(BuyOrderId buyOrderId, DateTime date, IEnumerable<OrderLine> lines, SupplierId supplierId);
}