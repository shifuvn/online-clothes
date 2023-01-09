using OnlineClothes.Application.Features.Image.Commands.UploadProduct;
using OnlineClothes.Application.Features.Image.Commands.UploadProfile;

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

	[HttpPost("product")]
	public async Task<IActionResult> UploadProduct([FromForm] UploadProductImageCommand request)
	{
		return HandleApiResponse(await Mediator.Send(request));
	}
}
