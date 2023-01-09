namespace OnlineClothes.Application.Persistence;

public interface ISkuRepository : IEfCoreRepository<ProductSku, string>
{
	Task<ProductSku> GetSkuDetailBySkuAsync(string sku, CancellationToken cancellationToken = default);
}
