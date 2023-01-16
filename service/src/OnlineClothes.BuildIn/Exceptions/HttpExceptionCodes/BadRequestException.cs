using Microsoft.AspNetCore.Http;

namespace OnlineClothes.BuildIn.Exceptions.HttpExceptionCodes;

public class BadRequestException : HttpException
{
	private const int Code = StatusCodes.Status400BadRequest;

	public BadRequestException() : this("Bad request")
	{
	}

	public BadRequestException(string message) : base(Code, message)
	{
	}
}
