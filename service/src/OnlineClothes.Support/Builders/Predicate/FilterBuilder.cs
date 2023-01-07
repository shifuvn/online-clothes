using System.Linq.Expressions;
using OnlineClothes.Support.Utilities.Extensions;

namespace OnlineClothes.Support.Builders.Predicate;

public class FilterBuilder<T>
{
	public FilterBuilder()
	{
		Empty();
	}

	public FilterBuilder(Expression<Func<T, bool>> expression)
	{
		Statement = expression;
	}

	public Expression<Func<T, bool>> Statement { get; private set; } = null!;

	public FilterBuilder<T> Empty()
	{
		Statement = _ => true;
		return this;
	}

	public FilterBuilder<T> False()
	{
		Statement = _ => false;
		return this;
	}

	public FilterBuilder<T> And(Expression<Func<T, bool>> other)
	{
		Statement = Statement.And(other);

		return this;
	}

	public FilterBuilder<T> Or(Expression<Func<T, bool>> other)
	{
		Statement = Statement.Or(other);
		return this;
	}

	public static FilterBuilder<T> Where(Expression<Func<T, bool>> expr)
	{
		return new FilterBuilder<T>(expr);
	}

	public static FilterBuilder<T> True()
	{
		return new FilterBuilder<T>().Empty();
	}
}
