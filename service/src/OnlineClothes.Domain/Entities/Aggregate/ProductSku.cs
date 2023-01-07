using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using OnlineClothes.Support.Entity.Event;

namespace OnlineClothes.Domain.Entities.Aggregate;

public class ProductSku : SupportDomainEvent, IEntity<string>
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public string Sku { get; set; } = null!;

	public int ProductId { get; set; }
	public int InStock { get; set; }
	public decimal AddOnPrice { get; set; }
	public ClotheSizeType Size { get; set; }
	public bool IsDeleted { get; set; }

	[ForeignKey("ProductId")] public Product Product { get; set; } = null!;

	[JsonIgnore] public virtual ICollection<OrderItem> OrderItems { get; set; } = new Collection<OrderItem>();
	[JsonIgnore] public virtual ICollection<CartItem> CartItems { get; set; } = new Collection<CartItem>();

	public DateTime CreatedAt { get; set; }
	public DateTime ModifiedAt { get; set; }

	public bool IsAvailable()
	{
		return InStock > 0 && !IsDeleted;
	}

	public void Disable()
	{
		IsDeleted = true;
	}

	public void Enable()
	{
		IsDeleted = false;
	}

	public void ImportStock(int number)
	{
		InStock += number;
	}

	public void ExportStock(int number)
	{
		InStock -= number;
	}

	public decimal GetPrice()
	{
		return Product.Price + AddOnPrice;
	}
}
