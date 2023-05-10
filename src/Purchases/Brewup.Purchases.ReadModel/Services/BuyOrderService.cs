using Brewup.Purchases.ReadModel.Entities;
using Brewup.Purchases.SharedKernel.DomainIds;
using Brewup.Purchases.SharedKernel.DTOs;
using Microsoft.Extensions.Logging;

namespace Brewup.Purchases.ReadModel.Services;

public class BuyOrderService : ServiceBase, IBuyOrderService
{
	public BuyOrderService(ILoggerFactory loggerFactory, IPersister persister) : base(loggerFactory, persister)
	{
	}

	public async Task CreateBuyOrder(BuyOrderId buyOrderId, DateTime date, IEnumerable<OrderLine> lines,
		SupplierId supplierId)
	{
		var buyOrder = BuyOrder.Create(buyOrderId, date, lines, supplierId);

		await Persister.Insert(buyOrder);
	}
}

public class BuyOrder : EntityBase
{
	public DateTime Date { get; private set; }
	public IEnumerable<OrderLine> Lines { get; private set; }
	public SupplierId SupplierId { get; private set; }

	public static BuyOrder Create(BuyOrderId buyOrderId, DateTime date, IEnumerable<OrderLine> lines, SupplierId supplierId)
	{
		return new BuyOrder(buyOrderId, date, lines, supplierId);
	}

	private BuyOrder(BuyOrderId buyOrderId, DateTime date, IEnumerable<OrderLine> lines, SupplierId supplierId)
	{
		Date = date;
		Lines = lines;
		SupplierId = supplierId;
		Id = buyOrderId.ToString();
	}
}