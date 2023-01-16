﻿using OnlineClothes.Domain.Entities.Aggregate;

namespace OnlineClothes.Domain.Entities;

public class OrderItem
{
	public int OrderId { get; set; }
	public string ProductSkuId { get; set; } = null!;
	public int Quantity { get; set; }
	public decimal Price { get; set; }

	[ForeignKey("OrderId")] public Order Order { get; set; } = null!;
	[ForeignKey("ProductSkuId")] public ProductSku ProductSku { get; set; } = null!;

	public decimal IntoMoney()
	{
		return Quantity * Price;
	}
}
