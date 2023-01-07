using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Orders.Queries.Listing;

public class
	OrderListingQueryHandler : IRequestHandler<OrderListingQuery,
		JsonApiResponse<PagingModel<OrderListingQueryViewModel>>>
{
	//private readonly IOrderRepository _orderRepository;
	//private readonly IUserContext _userContext;


	//public OrderListingQueryHandler(IUserContext userContext, IOrderRepository orderRepository)
	//{
	//	_userContext = userContext;
	//	_orderRepository = orderRepository;
	//}

	public async Task<JsonApiResponse<PagingModel<OrderListingQueryViewModel>>> Handle(OrderListingQuery request,
		CancellationToken cancellationToken)
	{
		//var findOptions = new FindOptions<OrderProduct, OrderProduct>
		//{
		//	Limit = request.PageSize,
		//	Skip = (request.PageIndex - 1) * request.PageSize,
		//	Sort = new BsonDocument { { "createdAt", 1 } }
		//};

		//var filterDef = PreparePredicate();

		//var ordersCursorTask = _orderRepository.FindAsync(filterDef, findOptions, cancellationToken);
		//var totalCountTask = _orderRepository.CountAsync(filterDef, cancellationToken);

		//await Task.WhenAll(ordersCursorTask, totalCountTask);

		//var model = new PagingModel<OrderListingQueryViewModel>(
		//	totalCountTask.Result,
		//	ordersCursorTask.Result.ToList()
		//		.Select(q => new OrderListingQueryViewModel(q.Id, q.CustomerId, q.Total, q.CreatedAt, q.State)));

		//return JsonApiResponse<PagingModel<OrderListingQueryViewModel>>.Success(data: model);
		throw new NotImplementedException();
	}

	//private FilterBuilder<OrderProduct> PreparePredicate()
	//{
	//	//if (!Enum.TryParse(_userContext.GetRole(), true, out AccountRole role))
	//	//{
	//	//	throw new InvalidOperationException($"Invalid role {role}");
	//	//}

	//	//var fb = new FilterBuilder<OrderProduct>();

	//	//if (role != AccountRole.Admin)
	//	//{
	//	//	fb = fb.And(q => q.CustomerId == _userContext.GetNameIdentifier());
	//	//}

	//	//return fb;
	//	throw new NotImplementedException();
	//}
}
