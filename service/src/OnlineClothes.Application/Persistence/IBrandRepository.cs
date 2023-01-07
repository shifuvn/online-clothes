namespace OnlineClothes.Application.Persistence;

public interface IBrandRepository : IEfCoreRepository<Brand, int>
{
	Task<Brand> GetByNameAsync(string name, CancellationToken cancellationToken = default);

	Task<bool> IsNameExistedAsync(string name, CancellationToken cancellationToken = default);
}
