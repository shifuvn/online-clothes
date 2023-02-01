using Microsoft.AspNetCore.Http;

namespace OnlineClothes.BuildIn.Exceptions.HttpExceptionCodes;

public class DataNotFoundException : HttpException
{
	private const int Code = StatusCodes.Status200OK;


	public DataNotFoundException() : this("Không tìm thấy yêu cầu")
	{
	}

	public DataNotFoundException(string message) : base(Code, message)
	{
	}
}
