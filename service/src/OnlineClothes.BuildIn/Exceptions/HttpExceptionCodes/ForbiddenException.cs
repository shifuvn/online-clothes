using Microsoft.AspNetCore.Http;

namespace OnlineClothes.BuildIn.Exceptions.HttpExceptionCodes;

public class ForbiddenException : HttpException
{
	private const int Code = StatusCodes.Status403Forbidden;


	public ForbiddenException() : base(Code, "Không có quyền truy cập")
	{
	}
}
