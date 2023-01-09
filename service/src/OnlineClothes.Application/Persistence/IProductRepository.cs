using OnlineClothes.Application.Persistence.Schemas.Products;

namespace OnlineClothes.Application.Persistence;

public interface IProductRepository : IEfCoreRepository<Product, int>
{
	Task<Product> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);

	Task EditOneAsync(
		int id,
		PutProductInRepoObject @object,
		CancellationToken cancellationToken = default);
}
