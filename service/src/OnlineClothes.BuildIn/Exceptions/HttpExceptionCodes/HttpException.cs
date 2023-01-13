namespace OnlineClothes.BuildIn.Exceptions.HttpExceptionCodes;

public class HttpException : Exception
{
	public int HttpCode;

	public HttpException(int httpCode, string message) : base(message)
	{
		HttpCode = httpCode;
	}
}
