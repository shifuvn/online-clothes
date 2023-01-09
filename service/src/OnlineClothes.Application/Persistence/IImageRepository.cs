using Microsoft.AspNetCore.Http;

namespace OnlineClothes.Application.Persistence;

public interface IImageRepository : IEfCoreRepository<ImageObject, int>
{
	Task UploadAccountAvatar(IFormFile file);

	Task<ImageObject> UploadProductFile(IFormFile file, CancellationToken cancellationToken = default);
}
