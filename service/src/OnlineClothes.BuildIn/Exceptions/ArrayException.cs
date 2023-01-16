using System.Diagnostics.CodeAnalysis;

namespace OnlineClothes.BuildIn.Exceptions;

public static class ArrayException
{
	public static void ThrowIfNullOrEmpty([NotNull] object? arg)
	{
		if (arg is null || ((Array)arg).Length == 0)
		{
			Throw($"{nameof(arg)} is an empty list or array");
		}
	}

	[DoesNotReturn]
	private static void Throw(string message)
	{
		throw new Exception(message);
	}
}
