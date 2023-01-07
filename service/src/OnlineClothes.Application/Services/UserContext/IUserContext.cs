namespace OnlineClothes.Application.Services.UserContext;

public interface IUserContext
{
	/// <summary>
	/// Get Id
	/// </summary>
	/// <returns></returns>
	int GetNameIdentifier();

	/// <summary>
	/// Get email
	/// </summary>
	/// <returns></returns>
	string GetAccountEmail();

	/// <summary>
	/// Get role
	/// </summary>
	/// <returns></returns>
	string GetRole();
}
