﻿using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace OnlineClothes.BuildIn.Utilities.Extensions;

public static class ExpressionExtension
{
	/// <summary>
	/// Combine 2 expression condition with AND operator
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="main"></param>
	/// <param name="other"></param>
	/// <returns></returns>
	[Pure]
	public static Expression<Func<T, bool>> And<T>(
		this Expression<Func<T, bool>> main,
		Expression<Func<T, bool>> other)
	{
		var parameter = VisitLeftAndRight(main, other, out var left, out var right);

		return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
	}

	/// <summary>
	/// Combine 2 expression condition with OR operator
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="main"></param>
	/// <param name="other"></param>
	/// <returns></returns>
	[Pure]
	public static Expression<Func<T, bool>> Or<T>(
		this Expression<Func<T, bool>> main,
		Expression<Func<T, bool>> other)
	{
		var parameter = VisitLeftAndRight(main, other, out var left, out var right);

		return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
	}

	private static ParameterExpression VisitLeftAndRight<T>(Expression<Func<T, bool>> main,
		Expression<Func<T, bool>> join,
		out Expression left, out Expression right)
	{
		var parameter = Expression.Parameter(typeof(T));

		var leftVisitor = new ReplaceExpressionVisitor(main.Parameters[0], parameter);
		left = leftVisitor.Visit(main.Body);

		var rightVisitor = new ReplaceExpressionVisitor(join.Parameters[0], parameter);
		right = rightVisitor.Visit(join.Body);
		return parameter;
	}
}

internal class ReplaceExpressionVisitor : ExpressionVisitor
{
	private readonly Expression _newValue;
	private readonly Expression _oldValue;

	public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
	{
		_oldValue = oldValue;
		_newValue = newValue;
	}

	public override Expression Visit(Expression? node)
	{
		if (node is null)
		{
			return _oldValue;
		}

		return node == _oldValue ? _newValue : base.Visit(node);
	}
}
