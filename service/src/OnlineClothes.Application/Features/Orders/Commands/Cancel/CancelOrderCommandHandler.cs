using Newtonsoft.Json;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.UserContext;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Exceptions.HttpExceptionCodes;

namespace OnlineClothes.Application.Features.Orders.Commands.Cancel;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<CancelOrderCommandHandler> _logger;
	private readonly IOrderRepository _orderRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IUserContext _userContext;

	public CancelOrderCommandHandler(IOrderRepository orderRepository,
		IUserContext userContext,
		IUnitOfWork unitOfWork,
		ILogger<CancelOrderCommandHandler> logger)
	{
		_orderRepository = orderRepository;
		_userContext = userContext;
		_unitOfWork = unitOfWork;
		_logger = logger;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(CancelOrderCommand request,
		CancellationToken cancellationToken)
	{
		var order = await _orderRepository.FindOneAsync(PrepareFilterBuilder(request), cancellationToken);
		if (order == null)
		{
			throw new ForbiddenException();
		}

		_orderRepository.Update(order);

		if (!order.UpdateState(OrderState.Canceled))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Không thể hủy đơn hàng");
		}

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		_logger.LogInformation("Cancel order: {object}", JsonConvert.SerializeObject(order));

		return JsonApiResponse<EmptyUnitResponse>.Success();
	}

	private FilterBuilder<Order> PrepareFilterBuilder(CancelOrderCommand request)
	{
		if (!Enum.TryParse(_userContext.GetRole(), true, out AccountRole role))
		{
			throw new InvalidOperationException($"Invalid role {role}");
		}

		var fb = new FilterBuilder<Order>(q => q.Id == request.OrderId);

		if (role == AccountRole.Client)
		{
			fb.And(q => q.AccountId == _userContext.GetNameIdentifier());
		}

		return fb;
	}
}
