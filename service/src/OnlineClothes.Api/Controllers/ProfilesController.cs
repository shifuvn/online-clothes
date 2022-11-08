using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineClothes.Application.Features.Profile.Commands.EditAvatar;
using OnlineClothes.Application.Features.Profile.Commands.EditInformation;
using OnlineClothes.Application.Features.Profile.Queries.FetchInformation;

namespace OnlineClothes.Api.Controllers;

[Authorize]
public class ProfilesController : ApiV1ControllerBase
{
	public ProfilesController(IMediator mediator) : base(mediator)
	{
	}

	[HttpGet]
	public async Task<IActionResult> FetchInformation(CancellationToken cancellationToken = default)
	{
		return ApiResponse(await Mediator.Send(new FetchInformationQuery(), cancellationToken));
	}

	[HttpPut("edit-info")]
	public async Task<IActionResult> EditInformation([FromBody] EditInformationCommand command,
		CancellationToken cancellationToken = default)
	{
		return ApiResponse(await Mediator.Send(command, cancellationToken));
	}

	[HttpPost("upload-avatar")]
	public async Task<IActionResult> EditAvatar([FromForm] EditAvatarCommand command)
	{
		return ApiResponse(await Mediator.Send(command));
	}
}
