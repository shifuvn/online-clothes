using System.Linq.Expressions;

namespace OnlineClothes.BuildIn.Exceptions;

public static class ExpressionException
{
	public static void ThrowIfInvalid<T>(Expression<Func<T, object>> expr)
	{
		ArgumentNullException.ThrowIfNull(expr, nameof(expr));

		if (expr.Body.NodeType == ExpressionType.Parameter)
		{
			throw new ArgumentException("Cannot generate property path from lambda parameter");
		}
	}
}
