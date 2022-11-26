using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Queries.Detail;

public class ProductDetailQuery : IRequest<JsonApiResponse<ProductDetailQueryViewModel>>
{
	public string ProductId { get; set; } = null!;
}
