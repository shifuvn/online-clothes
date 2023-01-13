using Microsoft.AspNetCore.Http;

namespace OnlineClothes.BuildIn.Exceptions.HttpExceptionCodes;

public class DataNotFoundException : HttpException
{
	private const int Code = StatusCodes.Status400BadRequest;


	public DataNotFoundException() : this("Data not found")
	{
	}

	public DataNotFoundException(string message) : base(Code, message)
	{
	}
}
