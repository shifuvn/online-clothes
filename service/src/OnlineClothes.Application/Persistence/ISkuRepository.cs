namespace OnlineClothes.Application.Persistence;

public interface ISkuRepository : IEfCoreRepository<ProductSku, string>
{
	/// <summary>
	/// Get SKU include image reference and product reference
	/// </summary>
	/// <param name="sku"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<ProductSku> GetSkuDetailBySkuAsync(string sku, CancellationToken cancellationToken = default);
}
