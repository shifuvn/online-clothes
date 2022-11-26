using System.Security.Claims;
using OnlineClothes.Domain.Entities;

namespace OnlineClothes.Infrastructure.Services.Auth.Abstracts;

public interface IAuthService
{
	string CreateJwtAccessToken(AccountUser account);

	List<Claim> ValidateJwtToken(string jwt);
}
