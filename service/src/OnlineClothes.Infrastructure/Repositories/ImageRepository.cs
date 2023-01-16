using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Infrastructure.Repositories;

public class ImageRepository : EfCoreRepositoryBase<ImageObject, int>, IImageRepository
{
	public ImageRepository(AppDbContext dbContext) : base(dbContext)
	{
	}
}
