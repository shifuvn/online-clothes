using Microsoft.AspNetCore.Http;

namespace OnlineClothes.Application.Features.Image.Commands.UploadProfile;

public class UploadAccountImageCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	// TODO: validate image
	public IFormFile File { get; set; } = null!;
}
