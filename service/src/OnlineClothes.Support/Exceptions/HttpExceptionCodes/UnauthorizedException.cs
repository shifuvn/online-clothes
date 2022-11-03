using Microsoft.AspNetCore.Http;

namespace OnlineClothes.Support.Exceptions.HttpExceptionCodes;

public class UnauthorizedException : HttpException
{
	private const int Code = StatusCodes.Status401Unauthorized;

	public UnauthorizedException() : base(Code, "You are not authorized")
	{
	}
}