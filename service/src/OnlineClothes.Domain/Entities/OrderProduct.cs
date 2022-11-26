using OnlineClothes.Domain.Attributes;
using OnlineClothes.Domain.Common;

namespace OnlineClothes.Domain.Entities;

[BsonCollection("orders")]
public class OrderProduct : RootDocumentBase
{
	public OrderProduct(string customerId, string customerEmail, string deliveryAddress)
	{
		CustomerId = customerId;
		CustomerEmail = customerEmail;
		DeliveryAddress = deliveryAddress;
	}

	public string CustomerId { get; set; }
	public string CustomerEmail { get; set; }
	public string DeliveryAddress { get; set; }

	public double Total { get; set; }

	public OrderState State { get; set; }

	public List<OrderItem> Items { get; set; } = new();

	public void UpdateState(OrderState newState)
	{
		switch (newState)
		{
			case OrderState.Delivering:
				if (State != OrderState.Pending)
				{
					throw new InvalidOperationException("Order state pipeline");
				}

				break;

			case OrderState.Canceled:
				if (State != OrderState.Pending || State != OrderState.Delivering)
				{
					throw new InvalidOperationException("Order state pipeline");
				}

				break;

			case OrderState.Success:
				if (State != OrderState.Delivering)
				{
					throw new InvalidOperationException("Order state pipeline");
				}

				break;

			case OrderState.Pending:
			default:
				throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
		}

		State = newState;
	}

	public void Add(OrderItem item)
	{
		Items.Add(item);
		Total += item.Price * item.Quantity;
	}

	public class OrderItem
	{
		public OrderItem(string id, int quantity, double price)
		{
			ProductId = id;
			Quantity = quantity;
			Price = price;
		}

		public string ProductId { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}