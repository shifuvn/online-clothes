using OnlineClothes.Domain.Entities;

namespace OnlineClothes.Application.Features.Product.Queries.Listing;

public class ListingProductQueryResultModel
{
	public ListingProductQueryResultModel(string productId, string productName, double price, string? imageUrl = null)
	{
		ProductId = productId;
		ProductName = productName;
		Price = price;
		ImageUrl = imageUrl;
	}

	public string ProductId { get; set; }
	public string ProductName { get; set; }
	public double Price { get; set; }
	public string? ImageUrl { get; set; }

	public static ListingProductQueryResultModel Create(ProductClothe input)
	{
		var model = new ListingProductQueryResultModel(input.Id, input.Name, input.Price,
			input.ImageUrls.FirstOrDefault());
		return model;
	}
}
