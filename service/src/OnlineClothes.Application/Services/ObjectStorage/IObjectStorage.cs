namespace OnlineClothes.Application.Services.ObjectStorage;

public interface IObjectStorage
{
	Task<string> UploadAsync(Models.ObjectStorage @object, CancellationToken cancellationToken = default);

	Task<Stream?> DownloadAsync(string objectIdentifier, CancellationToken cancellationToken = default);

	Task<bool> DeleteAsync(string objectIdentifier, CancellationToken cancellationToken = default);

	Task<bool> DeleteManyAsync(IEnumerable<string> objectIdentifiers, CancellationToken cancellationToken = default);
}
