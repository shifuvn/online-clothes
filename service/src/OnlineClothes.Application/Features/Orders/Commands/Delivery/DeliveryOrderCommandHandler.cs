using Newtonsoft.Json;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Orders.Commands.Delivery;

public class DeliveryOrderCommandHandler : IRequestHandler<DeliveryOrderCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<DeliveryOrderCommandHandler> _logger;
	private readonly IOrderRepository _orderRepository;
	private readonly IUnitOfWork _unitOfWork;

	public DeliveryOrderCommandHandler(
		IUnitOfWork unitOfWork,
		IOrderRepository orderRepository,
		ILogger<DeliveryOrderCommandHandler> logger)
	{
		_unitOfWork = unitOfWork;
		_orderRepository = orderRepository;
		_logger = logger;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(DeliveryOrderCommand request,
		CancellationToken cancellationToken)
	{
		var order = await _orderRepository.GetOneByUserContext(request.OrderId, cancellationToken);

		_orderRepository.Update(order);
		if (!order.UpdateState(OrderState.Delivering))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Không thể giao đơn hàng");
		}

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		_logger.LogInformation("Delivering order: {object}", JsonConvert.SerializeObject(order));

		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
