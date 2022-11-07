using OnlineClothes.Support.Utilities;

namespace OnlineClothes.Infrastructure.Services.Storage.Models;

public class CloudObjectFile
{
	public CloudObjectFile(Stream stream, string name, string prefixDirectory, string? contentType = null)
	{
		Stream = stream;
		Name = name;
		PrefixDirectory = prefixDirectory;

		contentType ??= MimeTypes.GetMimeType(name);
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
