using MediatR;
using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.UserContext.Abstracts;
using OnlineClothes.Persistence.Extensions;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Exceptions.HttpExceptionCodes;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Order.Commands.Cancel;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IOrderRepository _orderRepository;
	private readonly IUserContext _userContext;

	public CancelOrderCommandHandler(IOrderRepository orderRepository, IUserContext userContext)
	{
		_orderRepository = orderRepository;
		_userContext = userContext;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(CancelOrderCommand request,
		CancellationToken cancellationToken)
	{
		var order = await _orderRepository.FindOneAsync(PrepareFilterBuilder(request), cancellationToken);
		if (order == null)
		{
			throw new ForbiddenException();
		}

		order.UpdateState(OrderState.Canceled);

		var updateResult = await _orderRepository.UpdateOneAsync(
			order.Id,
			update => update.Set(q => q.State, order.State), cancellationToken: cancellationToken);

		return updateResult.Any()
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}

	private FilterBuilder<OrderProduct> PrepareFilterBuilder(CancelOrderCommand request)
	{
		if (!Enum.TryParse(_userContext.GetRole(), true, out AccountRole role))
		{
			throw new InvalidOperationException($"Invalid role {role}");
		}

		var fb = new FilterBuilder<OrderProduct>(q => q.Id == request.OrderId);

		if (role == AccountRole.Client)
		{
			fb.And(q => q.CustomerId == _userContext.GetNameIdentifier());
		}

		return fb;
	}
}