using System.Text.Json.Serialization;

namespace OnlineClothes.Support.HttpResponse;

public class JsonApiResponse<TResponse> where TResponse : class
{
	public JsonApiResponse(int status)
	{
		Status = status;
	}

	public JsonApiResponse(int status, string? message = null, TResponse? data = null, object? errors = default) :
		this(status)
	{
		Message = message;
		Data = data;
		Errors = errors;
	}

	public int Status { get; set; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Message { get; set; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public TResponse? Data { get; set; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public object? Errors { get; set; }

	[JsonIgnore] public bool IsError => Status is >= 400 and <= 599;

	public static JsonApiResponse<TResponse> Success(string message, TResponse? data = null)
	{
		return new JsonApiResponse<TResponse>(200, message, data);
	}

	public static JsonApiResponse<TResponse> Success(TResponse? data = null)
	{
		return new JsonApiResponse<TResponse>(200, null, data);
	}

	public static JsonApiResponse<TResponse> Fail(string? message = null)
	{
		message ??= "Cannot find anything!";
		return new JsonApiResponse<TResponse>(400, message);
	}
}

public class EmptyUnitResponse
{
}
