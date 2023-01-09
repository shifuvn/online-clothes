using OnlineClothes.Application.Features.Image.Commands.UploadProfileImage;

namespace OnlineClothes.Api.Controllers.V1;

public class ImagesController : ApiV1ControllerBase
{
	public ImagesController(IMediator mediator) : base(mediator)
	{
	}

	[HttpPost("profile/avatar")]
	public async Task<IActionResult> UploadAccountAvatar([FromForm] UploadAccountImageCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}
}
