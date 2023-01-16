namespace OnlineClothes.Application.Features.Products.Commands.PromoteThumbnail;

public class PromoteProductThumbnailCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public int ProductId { get; set; }
	public int ImageId { get; set; }
}
