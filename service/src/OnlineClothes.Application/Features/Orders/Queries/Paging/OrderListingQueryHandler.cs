using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.UserContext;
using OnlineClothes.Domain.Paging;
using OnlineClothes.Support.Builders.Predicate;

namespace OnlineClothes.Application.Features.Orders.Queries.Paging;

public class OrderListingQueryHandler : IRequestHandler<OrderListingQuery, JsonApiResponse<PagingModel<OrderDto>>>
{
	private readonly IOrderRepository _orderRepository;
	private readonly IUserContext _userContext;


	public OrderListingQueryHandler(IUserContext userContext, IOrderRepository orderRepository)
	{
		_userContext = userContext;
		_orderRepository = orderRepository;
	}

	public async Task<JsonApiResponse<PagingModel<OrderDto>>> Handle(OrderListingQuery request,
		CancellationToken cancellationToken)
	{
		var data = await _orderRepository.PagingAsync(
			PreparePredicate(),
			new PagingRequest(request.PageIndex, request.PageSize),
			SelectorFunc(),
			null,
			IncludeProps(),
			cancellationToken);

		return JsonApiResponse<PagingModel<OrderDto>>.Success(data: data);
	}

	private static IEnumerable<string> IncludeProps()
	{
		return new[] { "Items.ProductSku.Product", "Account" };
	}

	private Func<IQueryable<Order>, IQueryable<OrderDto>> SelectorFunc()
	{
		return queryable => queryable.Select(order => OrderDto.ToModel(order));
	}

	private FilterBuilder<Order> PreparePredicate()
	{
		if (!Enum.TryParse(_userContext.GetRole(), true, out AccountRole role))
		{
			throw new InvalidOperationException($"Invalid role {role}");
		}

		var fb = new FilterBuilder<Order>();

		if (role != AccountRole.Admin)
		{
			fb = fb.And(q => q.AccountId == _userContext.GetNameIdentifier());
		}

		return fb;
	}
}
