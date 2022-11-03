using MediatR;

namespace OnlineClothes.Support.HttpResponse;

public class JsonResponse
{
	public JsonResponse(int status)
	{
		Status = status;
	}

	public JsonResponse(int status, string? message = default, object? data = default, object? errors = default) :
		this(status)
	{
		Message = message;
		Data = data;
		Errors = errors;
	}

	public int Status { get; set; }
	public string? Message { get; set; }
	public object? Data { get; set; }
	public object? Errors { get; set; }

	public static JsonResponse Success(string? message = null, object? data = null)
	{
		data ??= Unit.Value;
		return new JsonResponse(200, message, data);
	}

	public static JsonResponse Success(object? data = null)
	{
		data ??= Unit.Value;
		return new JsonResponse(200, null, data);
	}

	public static JsonResponse Fail(string? message = null)
	{
		message ??= "Cannot find anything!";
		return new JsonResponse(400, message);
	}
}