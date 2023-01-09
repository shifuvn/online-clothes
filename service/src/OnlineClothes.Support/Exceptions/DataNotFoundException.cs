using System.Diagnostics.CodeAnalysis;

namespace OnlineClothes.Support.Exceptions;

public class DataNotFoundException
{
	public static void ThrowIfNull([NotNull] object? argument, string? message = null)
	{
		if (argument is null)
		{
			Throw(message);
		}
	}

	[DoesNotReturn]
	private static void Throw(string? message = null)
	{
		if (string.IsNullOrEmpty(message))
		{
			throw new HttpExceptionCodes.DataNotFoundException();
		}

		throw new HttpExceptionCodes.DataNotFoundException(message);
	}
}
