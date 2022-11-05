namespace OnlineClothes.Application.Features.Accounts.Commands.SignIn;

public sealed class SignInCommandResult
{
	public SignInCommandResult(string accessToken)
	{
		AccessToken = accessToken;
	}

	public string AccessToken { get; init; }
}
