using Brewup.Purchases.SharedKernel.DomainIds;
using Brewup.Purchases.SharedKernel.DTOs;

namespace Brewup.Purchases.ReadModel.Entities;

public class PurchaseOrder : EntityBase
{
	public DateTime Date { get; private set; }
	public IEnumerable<OrderLine> Lines { get; private set; }
	public SupplierId SupplierId { get; private set; }
	public OrderStatus Status { get; set; }

	public static PurchaseOrder Create(PurchaseOrderId purchaseOrderId, DateTime date, IEnumerable<OrderLine> lines,
		SupplierId supplierId)
	{
		return new PurchaseOrder(purchaseOrderId, date, lines, supplierId);
	}

	private PurchaseOrder(PurchaseOrderId purchaseOrderId, DateTime date, IEnumerable<OrderLine> lines,
		SupplierId supplierId)
	{
		Date = date;
		Lines = lines;
		SupplierId = supplierId;
		Id = purchaseOrderId.ToString();
		Status = OrderStatus.Created;
	}
}