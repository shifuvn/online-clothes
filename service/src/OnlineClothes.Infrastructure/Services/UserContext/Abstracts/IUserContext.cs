namespace OnlineClothes.Infrastructure.Services.UserContext.Abstracts;

public interface IUserContext
{
	string GetNameIdentifier();

	string GetAccountEmail();

	string GetRole();
}
