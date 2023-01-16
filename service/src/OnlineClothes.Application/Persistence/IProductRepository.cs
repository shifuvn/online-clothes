using OnlineClothes.Application.Persistence.Schemas.Products;

namespace OnlineClothes.Application.Persistence;

public interface IProductRepository : IEfCoreRepository<Product, int>
{
	Task EditOneAsync(
		int id,
		PutProductInRepoObject @object,
		CancellationToken cancellationToken = default);
}
