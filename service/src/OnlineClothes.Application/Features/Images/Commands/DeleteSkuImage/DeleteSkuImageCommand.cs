namespace OnlineClothes.Application.Features.Images.Commands.DeleteSkuImage;

public class DeleteSkuImageCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Sku { get; set; } = null!;
}
