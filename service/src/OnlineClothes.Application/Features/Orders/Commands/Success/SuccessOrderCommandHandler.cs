namespace OnlineClothes.Application.Features.Orders.Commands.Success;

public class SuccessOrderCommandHandler : IRequestHandler<SuccessOrderCommand, JsonApiResponse<EmptyUnitResponse>>
{
	//private readonly IOrderRepository _orderRepository;

	//public SuccessOrderCommandHandler(IOrderRepository orderRepository)
	//{
	//	_orderRepository = orderRepository;
	//}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(SuccessOrderCommand request,
		CancellationToken cancellationToken)
	{
		//var order = await _orderRepository.GetOneAsync(request.OrderId, cancellationToken);
		//order.UpdateState(OrderState.Success);

		//var updateResult = await _orderRepository.UpdateOneAsync(
		//	order.Id,
		//	update => update.Set(q => q.State, order.State), cancellationToken: cancellationToken);

		//return updateResult.Any()
		//	? JsonApiResponse<EmptyUnitResponse>.Success()
		//	: JsonApiResponse<EmptyUnitResponse>.Fail();

		throw new NotImplementedException();
	}
}
