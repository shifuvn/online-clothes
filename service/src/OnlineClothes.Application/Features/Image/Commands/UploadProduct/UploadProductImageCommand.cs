using Microsoft.AspNetCore.Http;

namespace OnlineClothes.Application.Features.Image.Commands.UploadProduct;

public class UploadProductImageCommand : IRequest<JsonApiResponse<int>>
{
	public IFormFile File { get; set; } = null!;
}
