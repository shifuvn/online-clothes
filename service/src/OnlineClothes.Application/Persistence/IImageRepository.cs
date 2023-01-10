using Microsoft.AspNetCore.Http;

namespace OnlineClothes.Application.Persistence;

public interface IImageRepository : IEfCoreRepository<ImageObject, int>
{
	Task AddAccountAvatarFileAsync(IFormFile file);

	Task<ImageObject?> AddProductImageFileAsync(IFormFile? file, CancellationToken cancellationToken = default);
}
