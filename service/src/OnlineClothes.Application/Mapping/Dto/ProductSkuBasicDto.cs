namespace OnlineClothes.Application.Mapping.Dto;

public class ProductSkuBasicDto
{
	public ProductSkuBasicDto(
		int productId,
		string sku,
		string name,
		decimal price,
		decimal addOnPrice,
		int inStock,
		bool isDeleted,
		string? imageUrl = null)
	{
		ProductId = productId;
		Sku = sku;
		Name = name;
		Price = price;
		AddOnPrice = addOnPrice;
		InStock = inStock;
		IsDeleted = isDeleted;
		ImageUrl = imageUrl;
	}

	public ProductSkuBasicDto(ProductSku domain) : this(
		domain.ProductId,
		domain.Sku,
		domain.Product.Name,
		domain.Product.Price,
		domain.AddOnPrice,
		domain.InStock,
		domain.IsDeleted,
		domain.Image?.Url)
	{
		CreatedAt = domain.CreatedAt;
		TotalPrice = domain.TotalPrice();
	}

	public int ProductId { get; set; }
	public string Sku { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
	public decimal AddOnPrice { get; set; }
	public decimal TotalPrice { get; set; }
	public int InStock { get; set; }
	public string? ImageUrl { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime CreatedAt { get; set; }
}
