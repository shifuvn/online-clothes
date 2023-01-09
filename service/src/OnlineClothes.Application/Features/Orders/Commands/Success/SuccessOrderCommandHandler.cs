using Newtonsoft.Json;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Orders.Commands.Success;

public class SuccessOrderCommandHandler : IRequestHandler<SuccessOrderCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<SuccessOrderCommandHandler> _logger;
	private readonly IOrderRepository _orderRepository;
	private readonly IUnitOfWork _unitOfWork;

	public SuccessOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork,
		ILogger<SuccessOrderCommandHandler> logger)
	{
		_orderRepository = orderRepository;
		_unitOfWork = unitOfWork;
		_logger = logger;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(SuccessOrderCommand request,
		CancellationToken cancellationToken)
	{
		var order = await _orderRepository.GetByIntKey(request.OrderId, cancellationToken);

		_orderRepository.Update(order);
		if (!order.UpdateState(OrderState.Success))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Không thể hoàn thành đơn hàng");
		}

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		_logger.LogInformation("Success order: {object}", JsonConvert.SerializeObject(order));

		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
