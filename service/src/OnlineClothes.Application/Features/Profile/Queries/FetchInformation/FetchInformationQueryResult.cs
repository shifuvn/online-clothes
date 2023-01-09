namespace OnlineClothes.Application.Features.Profile.Queries.FetchInformation;

public class FetchInformationQueryResult
{
	public string Email { get; set; } = null!;
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string FullName => Fullname.Create(FirstName, LastName).FullName;
	public string? Address { get; set; }
	public string? PhoneNumber { get; set; }
	public string? ImageUrl { get; set; }
	public DateTime LastLogin { get; set; }

	public static FetchInformationQueryResult ToModel(AccountUser accountUser)
	{
		return new FetchInformationQueryResult
		{
			Email = accountUser.Email,
			FirstName = accountUser.FirstName,
			LastName = accountUser.LastName,
			ImageUrl = accountUser.ImageUrl,
			LastLogin = accountUser.LastLogin,
			PhoneNumber = accountUser.PhoneNumber,
			Address = accountUser.Address
		};
	}
}
