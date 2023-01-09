using System.Security.Claims;

namespace OnlineClothes.Application.Services.Auth;

public interface IAuthorizeService
{
	string CreateJwtAccessToken(AccountUser account);

	List<Claim> ValidateJwtToken(string jwt);
}
