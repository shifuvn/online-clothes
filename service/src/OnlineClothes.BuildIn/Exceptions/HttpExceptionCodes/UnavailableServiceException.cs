using Microsoft.AspNetCore.Http;

namespace OnlineClothes.BuildIn.Exceptions.HttpExceptionCodes;

public class UnavailableServiceException : HttpException
{
	private const int Code = StatusCodes.Status503ServiceUnavailable;

	public UnavailableServiceException() : this("Some service is not available now")
	{
	}

	public UnavailableServiceException(string message) : base(Code, message)
	{
	}
}
