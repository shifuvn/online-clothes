namespace OnlineClothes.Application.Features.Accounts.Commands.SignIn;

public sealed class SignInAccountCommandResult
{
	public SignInAccountCommandResult(string accessToken)
	{
		AccessToken = accessToken;
	}

	public string AccessToken { get; init; }
}
