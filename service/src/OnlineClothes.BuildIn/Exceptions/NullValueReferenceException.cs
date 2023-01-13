using System.Diagnostics.CodeAnalysis;

namespace OnlineClothes.BuildIn.Exceptions;

public class NullValueReferenceException
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
		throw new NullReferenceException(message);
	}
}
