using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Order.Commands.Cancel;

public class CancelOrderCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public CancelOrderCommand(string orderId)
	{
		OrderId = orderId;
	}

	public string OrderId { get; set; }
}