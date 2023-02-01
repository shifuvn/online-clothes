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

	[JsonIgnore] public bool IsError { get; set; }

	public static JsonApiResponse<TResponse> Success(int? code = null, string? message = null,
		TResponse? data = default, bool isError = false)
	{
		return new JsonApiResponse<TResponse>(code ?? 200, message, data)
		{
			IsError = isError
		};
	}

	public static JsonApiResponse<TResponse> Created(string? message = null, TResponse? data = default)
	{
		return new JsonApiResponse<TResponse>(StatusCodes.Status201Created, message, data);
	}

	public static JsonApiResponse<TResponse> Fail(string? message = null, TResponse? data = default,
		bool isError = true)
	{
		message ??= "Không tìm thấy yêu cầu";
		return new JsonApiResponse<TResponse>(400, message, data)
		{
			IsError = isError
		};
	}
}

public class EmptyUnitResponse
{
}
