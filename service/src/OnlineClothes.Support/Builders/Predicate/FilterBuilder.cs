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

	public FilterBuilder<T> True(Expression<Func<T, bool>>? expr = default)
	{
		if (expr is not null)
		{
			Statement = expr;
		}

		return this;
	}

	public FilterBuilder<T> False()
	{
		Statement = _ => false;
		return this;
	}

	public FilterBuilder<T> And(Expression<Func<T, bool>> other)
	{
		Statement.And(other);

		return this;
	}

	public FilterBuilder<T> Or(Expression<Func<T, bool>> other)
	{
		Statement.Or(other);
		return this;
	}
}