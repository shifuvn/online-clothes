using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.UserContext;
using OnlineClothes.Support.Builders.Predicate;

namespace OnlineClothes.Application.Features.Orders.Queries.Detail;

public class OrderDetailQueryHandler : IRequestHandler<OrderDetailQuery, JsonApiResponse<OrderDto>>
{
	private readonly IOrderRepository _orderRepository;
	private readonly IUserContext _userContext;

	public OrderDetailQueryHandler(IUserContext userContext, IOrderRepository orderRepository)
	{
		_userContext = userContext;
		_orderRepository = orderRepository;
	}

	public async Task<JsonApiResponse<OrderDto>> Handle(OrderDetailQuery request,
		CancellationToken cancellationToken)
	{
		var userRole = _userContext.GetRole();
		var order = await _orderRepository.FindOneAsync(PreparePredicate(userRole, request.OrderId), cancellationToken);

		if (order is null)
		{
			return JsonApiResponse<OrderDto>.Fail();
		}

		var viewModel = OrderDto.ToModel(order);
		return JsonApiResponse<OrderDto>.Success(data: viewModel);
	}

	private FilterBuilder<Order> PreparePredicate(string role, int orderId)
	{
		if (!Enum.TryParse(role, true, out AccountRole enumRole))
		{
			throw new InvalidOperationException($"invalid role {role}");
		}

		var filter = new FilterBuilder<Order>(q => q.Id == orderId);

		if (enumRole == AccountRole.Client)
		{
			filter = filter.And(q => q.AccountId == _userContext.GetNameIdentifier());
		}

		return filter;
	}
}
