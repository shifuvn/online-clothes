using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Commands.Delete;

public class DeleteProductCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public DeleteProductCommand(string productId)
	{
		ProductId = productId;
	}

	public string ProductId { get; set; }
}
