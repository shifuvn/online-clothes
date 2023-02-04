namespace OnlineClothes.Application.Features.Skus.Queries.OtherSku;

public class GetAllSkuInTheSameProductQueryViewModel
{
	public GetAllSkuInTheSameProductQueryViewModel(string sku, string? displaySkuName)
	{
		Sku = sku;
		DisplaySkuName = displaySkuName;
	}

	public string Sku { get; set; }
	public string? DisplaySkuName { get; set; }
}
