using Microsoft.AspNetCore.Http;
using OnlineClothes.Support.Utilities;

namespace OnlineClothes.Application.Services.ObjectStorage.Models;

public class ObjectStorage
{
	public ObjectStorage(Stream stream, string name, string prefixDirectory, string? contentType = null)
	{
		Stream = stream;
		Name = name;
		PrefixDirectory = prefixDirectory;

		contentType ??= MimeTypes.GetMimeType(name);
		ContentType = contentType;
	}

	public ObjectStorage(
		IFormFile file,
		string prefixDirectory,
		string? fileName = null,
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

	/// <summary>
	/// Get identifier key from url.
	/// </summary>
	/// <param name="url"></param>
	/// <param name="separator"></param>
	/// <param name="skipping"></param>
	/// <returns></returns>
	public static string GetIdentifierKey(string url,
		char separator = '/',
		int skipping = ObjectStorageConstant.AwsS3SkipSeparatorFromUrl)
	{
		var urlSplit = url.Split(separator);
		var key = string.Join('/', urlSplit.Skip(skipping));
		return key;
	}
}
