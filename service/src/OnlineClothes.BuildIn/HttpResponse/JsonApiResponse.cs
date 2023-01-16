using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace OnlineClothes.BuildIn.HttpResponse;

public class JsonApiResponse<TResponse>
{
	public JsonApiResponse(int status)
	{
		Status = status;
	}

	public JsonApiResponse(int status, string? message = null, TResponse? data = default, object? errors = default) :
		this(status)
	{
		Message = message;
		Data = data;
		Errors = errors;
	}

	public int Status { get; set; }

	public string? Message { get; set; }

	public TResponse? Data { get; set; }

	public object? Errors { get; set; }

	[JsonIgnore] public bool IsError => Status is >= 400 and <= 599;

	public static JsonApiResponse<TResponse> Success(int? code = null, string? message = null,
		TResponse? data = default)
	{
		return new JsonApiResponse<TResponse>(code ?? 200, message, data);
	}

	public static JsonApiResponse<TResponse> Created(string? message = null, TResponse? data = default)
	{
		return new JsonApiResponse<TResponse>(StatusCodes.Status201Created, message, data);
	}

	public static JsonApiResponse<TResponse> Fail(string? message = null, TResponse? data = default)
	{
		message ??= "Cannot find anything!";
		return new JsonApiResponse<TResponse>(400, message, data);
	}
}

public class EmptyUnitResponse
{
}
