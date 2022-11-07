using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Profile.Commands.EditInformation;

public class EditInformationCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
}
