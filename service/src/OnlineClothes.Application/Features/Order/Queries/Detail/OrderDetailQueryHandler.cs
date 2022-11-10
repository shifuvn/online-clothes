using MediatR;
using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.UserContext.Abstracts;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Order.Queries.Detail;

public class OrderDetailQueryHandler : IRequestHandler<OrderDetailQuery, JsonApiResponse<OrderProduct>>
{
	private readonly IOrderRepository _orderRepository;
	private readonly IUserContext _userContext;

	public OrderDetailQueryHandler(IUserContext userContext, IOrderRepository orderRepository)
	{
		_userContext = userContext;
		_orderRepository = orderRepository;
	}

	public async Task<JsonApiResponse<OrderProduct>> Handle(OrderDetailQuery request,
		CancellationToken cancellationToken)
	{
		var userRole = _userContext.GetRole();
		var order = await _orderRepository.FindOneAsync(PreparePredicate(userRole, request.OrderId), cancellationToken);

		return JsonApiResponse<OrderProduct>.Success(data: order);
	}

	private FilterBuilder<OrderProduct> PreparePredicate(string role, string orderId)
	{
		if (!Enum.TryParse(role, true, out AccountRole enumRole))
		{
			throw new InvalidOperationException($"invalid role {role}");
		}

		var filter = new FilterBuilder<OrderProduct>(q => q.Id == orderId);

		if (enumRole == AccountRole.Client)
		{
			filter = filter.And(q => q.CustomerId == _userContext.GetNameIdentifier());
		}

		return filter;
	}
}
