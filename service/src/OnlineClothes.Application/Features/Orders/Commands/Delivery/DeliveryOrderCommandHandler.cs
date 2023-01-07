namespace OnlineClothes.Application.Features.Orders.Commands.Delivery;

public class DeliveryOrderCommandHandler : IRequestHandler<DeliveryOrderCommand, JsonApiResponse<EmptyUnitResponse>>
{
	//private readonly IOrderRepository _orderRepository;
	//private readonly IProductRepository _productRepository;

	//public DeliveryOrderCommandHandler(IOrderRepository orderRepository,
	//	IProductRepository productRepository)
	//{
	//	_orderRepository = orderRepository;
	//	_productRepository = productRepository;
	//}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(DeliveryOrderCommand request,
		CancellationToken cancellationToken)
	{
		//var order = await _orderRepository.FindOneAsync(new FilterBuilder<OrderProduct>(q => q.Id == request.OrderId),
		//	cancellationToken);
		//if (order is null)
		//{
		//	return JsonApiResponse<EmptyUnitResponse>.Fail();
		//}

		//order.UpdateState(OrderState.Delivering);
		//var updateResult = await _orderRepository.UpdateOneAsync(
		//	order.Id,
		//	update => update.Set(q => q.State, order.State),
		//	cancellationToken: cancellationToken);

		//if (!updateResult.Any())
		//{
		//	return JsonApiResponse<EmptyUnitResponse>.Fail();
		//}

		//await PostActionDeliveryOrder(order);
		//return JsonApiResponse<EmptyUnitResponse>.Success();

		throw new NotImplementedException();
	}


	//private async Task PostActionDeliveryOrder(OrderProduct order)
	//{
	//	//var tasks = new List<Task>();
	//	//var orderIdQuantity = order.Items.ToDictionary(q => q.ProductId, q => q.Quantity);
	//	//var orderItemIds = orderIdQuantity.Select(q => q.Key).ToList();
	//	//var products = (await _productRepository.FindAsync(
	//	//		FilterBuilder<ProductClothe>.Where(q => orderItemIds.Contains(q.Id))))
	//	//	.ToList();

	//	//foreach (var productClothe in products)
	//	//{
	//	//	productClothe.Stock -= orderIdQuantity[productClothe.Id];
	//	//	tasks.Add(_productRepository.UpdateOneAsync(productClothe.Id,
	//	//		update => update.Set(q => q.Stock, productClothe.Stock)));
	//	//}

	//	//await Task.WhenAll(tasks);
	//}
}
