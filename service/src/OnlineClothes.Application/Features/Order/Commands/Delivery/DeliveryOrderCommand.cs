using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Order.Commands.Delivery;

public class DeliveryOrderCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public DeliveryOrderCommand(string orderId)
	{
		OrderId = orderId;
	}

	public string OrderId { get; set; }
}