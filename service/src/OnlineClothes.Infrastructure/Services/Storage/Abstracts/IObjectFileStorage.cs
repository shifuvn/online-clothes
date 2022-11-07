using OnlineClothes.Infrastructure.Services.Storage.Models;

namespace OnlineClothes.Infrastructure.Services.Storage.Abstracts;

public interface IObjectFileStorage
{
	Task<string?> UploadAsync(CloudObjectFile @object);

	Task<Stream?> DownloadAsync(string objectIdentifier);

	Task<bool> DeleteAsync(string objectIdentifier);

	Task<bool> DeleteManyAsync(IEnumerable<string> objectIdentifiers);
}
