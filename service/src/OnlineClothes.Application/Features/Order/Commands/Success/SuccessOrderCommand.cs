using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Order.Commands.Success;

public class SuccessOrderCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public SuccessOrderCommand(string orderId)
	{
		OrderId = orderId;
	}

	public string OrderId { get; set; }
}