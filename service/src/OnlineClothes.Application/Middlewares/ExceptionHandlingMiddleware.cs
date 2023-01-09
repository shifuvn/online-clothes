using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using OnlineClothes.Support.Exceptions.HttpExceptionCodes;
using JsonSerializerOptions = OnlineClothes.Support.JsonSerializer.JsonSerializerOptions;

namespace OnlineClothes.Application.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
	private readonly ILogger<ExceptionHandlingMiddleware> _logger;


	public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
	{
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (Exception e)
		{
			_logger.LogError(e, "{Message}", e.Message);
			await HandleExceptionAsync(context, e);
		}
	}

	private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
	{
		var (statusCode, trace) = GetStatusCode(exception);

		var responseEx = new JsonApiResponse<EmptyUnitResponse>(statusCode, exception.Message, default, trace);

		httpContext.Response.ContentType = "application/json";
		httpContext.Response.StatusCode = statusCode;

		var json = JsonSerializer.Serialize(responseEx, JsonSerializerOptions.Default);

		await httpContext.Response.WriteAsync(json);
	}

	private static (int, object?) GetStatusCode(Exception exception)
	{
		// TODO: handle environment for dev message
		return exception switch
		{
			HttpException httpEx => (httpEx.HttpCode, httpEx.StackTrace),
			ValidationException validationEx => (StatusCodes.Status400BadRequest,
				validationEx.Errors.ToDictionary(q => q.PropertyName, q => q.ErrorMessage)),
			ArgumentNullException argNullException => (StatusCodes.Status400BadRequest, argNullException.Message),
			_ => (500, default)
		};
	}
}
