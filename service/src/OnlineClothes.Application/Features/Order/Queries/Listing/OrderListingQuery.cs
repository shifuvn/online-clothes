using MediatR;
using OnlineClothes.Domain.Paging;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Order.Queries.Listing;

public class OrderListingQuery : PagingRequest, IRequest<JsonApiResponse<PagingModel<OrderListingQueryViewModel>>>
{
}
