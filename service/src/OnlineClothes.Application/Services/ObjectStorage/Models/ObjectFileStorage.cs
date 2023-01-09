using Microsoft.AspNetCore.Http;
using OnlineClothes.Support.Utilities;

namespace OnlineClothes.Application.Services.ObjectStorage.Models;

public class ObjectFileStorage
{
	public ObjectFileStorage(Stream stream, string name, string prefixDirectory, string? contentType = null)
	{
		Stream = stream;
		Name = name;
		PrefixDirectory = prefixDirectory;

		contentType ??= MimeTypes.GetMimeType(name);
		ContentType = contentType;
	}

	public ObjectFileStorage(IFormFile file, string prefixDirectory, string? fileName = null,
		string? contentType = null)
	{
		Stream = file.OpenReadStream();
		PrefixDirectory = prefixDirectory;

		fileName ??= file.FileName;
		Name = fileName;

		contentType ??= file.ContentType;
		ContentType = contentType;
	}

	public string IdentifierKey => Util.Url.RemoveSpecialCharacters(CombinePrefixDirectory(PrefixDirectory, Name));

	public Stream Stream { get; }
	public string Name { get; }
	public string PrefixDirectory { get; }
	public string ContentType { get; }


	public static string CombinePrefixDirectory(params string[] partition)
	{
		return string.Join('/', partition);
	}
}
