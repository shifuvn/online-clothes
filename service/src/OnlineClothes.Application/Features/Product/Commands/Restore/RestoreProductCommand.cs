using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Commands.Restore;

public class RestoreProductCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public RestoreProductCommand(string productId)
	{
		ProductId = productId;
	}

	public string ProductId { get; set; }
}
