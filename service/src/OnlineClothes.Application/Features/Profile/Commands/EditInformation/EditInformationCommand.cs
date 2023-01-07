namespace OnlineClothes.Application.Features.Profile.Commands.EditInformation;

public class EditInformationCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string FirstName { get; init; } = null!;
	public string LastName { get; init; } = null!;
	public string? PhoneNumber { get; init; }
	public string? Address { get; init; }

	public void Map(AccountUser account)
	{
		account.FirstName = FirstName;
		account.LastName = LastName;
		account.PhoneNumber = PhoneNumber;
		account.Address = Address;
	}
}
