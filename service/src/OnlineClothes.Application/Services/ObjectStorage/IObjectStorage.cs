namespace OnlineClothes.Application.Services.ObjectStorage;

public interface IObjectStorage
{
	/// <summary>
	/// Upload a object file to storage space
	/// </summary>
	/// <param name="object"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<string> UploadAsync(Models.ObjectStorage @object, CancellationToken cancellationToken = default);

	/// <summary>
	/// Replace a file content existed in storage
	/// </summary>
	/// <param name="stream"></param>
	/// <param name="identifierKey"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task ReplaceAsync(Stream stream, string identifierKey, string? contentType = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Download stream content of storage object file
	/// </summary>
	/// <param name="objectIdentifier"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<Stream?> DownloadAsync(string objectIdentifier, CancellationToken cancellationToken = default);

	/// <summary>
	/// Delete object from storage space
	/// </summary>
	/// <param name="objectIdentifier"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<bool> DeleteAsync(string objectIdentifier, CancellationToken cancellationToken = default);

	/// <summary>
	/// Delete IEnumerable of object from storage space
	/// </summary>
	/// <param name="objectIdentifiers"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<bool> DeleteManyAsync(IEnumerable<string> objectIdentifiers, CancellationToken cancellationToken = default);
}
