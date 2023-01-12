using Microsoft.AspNetCore.Authorization;
using OnlineClothes.Application.Features.Images.Commands.UploadProfile;
using OnlineClothes.Application.Features.Profile.Commands.EditInformation;
using OnlineClothes.Application.Features.Profile.Queries.FetchInformation;

namespace OnlineClothes.Api.Controllers.V1;

[Authorize]
public class ProfilesController : ApiV1ControllerBase
{
	public ProfilesController(IMediator mediator) : base(mediator)
	{
	}

	[HttpGet]
	public async Task<IActionResult> FetchInformation(CancellationToken cancellationToken = default)
	{
		return HandleApiResponse(await Mediator.Send(new FetchInformationQuery(), cancellationToken));
	}

	[HttpPut("edit-info")]
	public async Task<IActionResult> EditInformation([FromBody] EditInformationCommand command,
		CancellationToken cancellationToken = default)
	{
		return HandleApiResponse(await Mediator.Send(command, cancellationToken));
	}


	[HttpPost("profile/avatar")]
	public async Task<IActionResult> UploadAccountAvatar([FromForm] UploadAccountImageCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}
}
