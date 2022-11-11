using MediatR;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Order.Queries.Detail;

public class OrderDetailQuery : IRequest<JsonApiResponse<OrderProduct>>
{
	public OrderDetailQuery(string orderId)
	{
		OrderId = orderId;
	}

	public string OrderId { get; set; }
}
