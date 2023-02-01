using Microsoft.AspNetCore.Http;

namespace OnlineClothes.BuildIn.Exceptions.HttpExceptionCodes;

public class UnauthorizedException : HttpException
{
	private const int Code = StatusCodes.Status401Unauthorized;

	public UnauthorizedException() : base(Code, "Chưa đăng nhập")
	{
	}
}
