using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Cart.Queries.GetInfo;

public class GetCartInfoQuery : IRequest<JsonApiResponse<GetCartInfoQueryViewModel>>
{
}
