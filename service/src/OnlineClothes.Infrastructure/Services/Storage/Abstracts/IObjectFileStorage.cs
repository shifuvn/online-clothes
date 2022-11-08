using OnlineClothes.Infrastructure.Services.Storage.Models;

namespace OnlineClothes.Infrastructure.Services.Storage.Abstracts;

public interface IObjectFileStorage
{
	Task<string> UploadAsync(CloudObjectFile @object, CancellationToken cancellationToken = default);

	Task<Stream?> DownloadAsync(string objectIdentifier, CancellationToken cancellationToken = default);

	Task<bool> DeleteAsync(string objectIdentifier, CancellationToken cancellationToken = default);

	Task<bool> DeleteManyAsync(IEnumerable<string> objectIdentifiers, CancellationToken cancellationToken = default);
}
