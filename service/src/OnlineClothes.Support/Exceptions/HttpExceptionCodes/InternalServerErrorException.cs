using Microsoft.AspNetCore.Http;

namespace OnlineClothes.Support.Exceptions.HttpExceptionCodes;

public class InternalServerErrorException : HttpException
{
	private const int Code = StatusCodes.Status500InternalServerError;


	public InternalServerErrorException() : this("Internal server error")
	{
	}

	public InternalServerErrorException(string message) : base(Code, message)
	{
	}
}