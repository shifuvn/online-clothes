using OnlineClothes.Domain.Attributes;

namespace OnlineClothes.Domain.Entities;

[BsonCollection("orders")]
public class OrderProduct : RootDocumentBase
{
	public OrderProduct(string customerEmail, string customerName, string deliveryAddress, double total,
		OrderDetail detail)
	{
		CustomerEmail = customerEmail;
		CustomerName = customerName;
		DeliveryAddress = deliveryAddress;
		Total = total;
		Detail = detail;
	}

	public string CustomerEmail { get; set; }
	public string CustomerName { get; set; }
	public string DeliveryAddress { get; set; }
	public double Total { get; set; }

	public OrderDetail Detail { get; set; }

	public class OrderDetail
	{
		public OrderDetail(string id, uint quantity, double price)
		{
			ProductId = id;
			Quantity = quantity;
			Price = price;
		}

		public string ProductId { get; set; }
		public uint Quantity { get; set; }
		public double Price { get; set; }
	}
}
