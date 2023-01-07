namespace OnlineClothes.Application.Features.Orders.Commands.Checkout;

public class
	CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, JsonApiResponse<CheckoutOrderCommandViewModel>>
{
	//private readonly IAccountRepository _accountRepository;
	//private readonly ICartRepository _cartRepository;
	//private readonly ILogger<CheckoutOrderCommandHandler> _logger;
	//private readonly IMailingService _mailingService;
	//private readonly IOrderRepository _orderRepository;
	//private readonly IProductRepository _productRepository;
	//private readonly IUserContext _userContext;


	//public CheckoutOrderCommandHandler(IUserContext userContext,
	//	ILogger<CheckoutOrderCommandHandler> logger,
	//	IProductRepository productRepository,
	//	IOrderRepository orderRepository,
	//	ICartRepository cartRepository,
	//	IAccountRepository accountRepository,
	//	IMailingService mailingService)
	//{
	//	_userContext = userContext;
	//	_logger = logger;
	//	_productRepository = productRepository;
	//	_orderRepository = orderRepository;
	//	_cartRepository = cartRepository;
	//	_accountRepository = accountRepository;
	//	_mailingService = mailingService;
	//}

	public async Task<JsonApiResponse<CheckoutOrderCommandViewModel>> Handle(CheckoutOrderCommand request,
		CancellationToken cancellationToken)
	{
		//var accountCart = await _cartRepository.FindOneAsync(
		//	FilterBuilder<AccountCart>.Where(q => q.AccountId == _userContext.GetNameIdentifier()), cancellationToken);
		//NullValueReferenceException.ThrowIfNull(accountCart, nameof(accountCart));

		//if (!accountCart.Items.Any())
		//{
		//	return JsonApiResponse<CheckoutOrderCommandViewModel>.Fail("Empty cart");
		//}

		//var itemsInCart = accountCart.Items.Select(q => new KeyValuePair<string, int>(q.ProductId, q.Quantity))
		//	.ToArray();
		//var itemInCardIds = itemsInCart.Select(x => x.Key).ToHashSet();

		//var productsFromCart = (await _productRepository.FindAsync(
		//		FilterBuilder<ProductClothe>.Where(q => itemInCardIds.Contains(q.Id)),
		//		cancellationToken: cancellationToken))
		//	.ToList()
		//	.ToDictionary(q => q.Id);

		//foreach (var (key, value) in itemsInCart)
		//{
		//	if (value > productsFromCart[key].Stock)
		//	{
		//		return JsonApiResponse<CheckoutOrderCommandViewModel>.Fail(data: new CheckoutOrderCommandViewModel(key),
		//			message: $"Sản phẩm {productsFromCart[key].Name} không đủ");
		//	}
		//}

		//var account = await _accountRepository.GetOneAsync(_userContext.GetNameIdentifier(), cancellationToken);
		//var order = new OrderProduct(account.Id.ToString(), account.Email, request.Address);
		//foreach (var (key, value) in itemsInCart)
		//{
		//	order.Add(new OrderProduct.OrderItem(key, value, productsFromCart[key].Price));
		//}

		//await Task.WhenAll(ClearCartAsync(accountCart, cancellationToken),
		//	CreateOrderTransactionAsync(order, cancellationToken),
		//	SendReceiptMail(itemsInCart, productsFromCart, order));

		//return JsonApiResponse<CheckoutOrderCommandViewModel>.Success();

		throw new NotImplementedException();
	}

	//private async Task CreateOrderTransactionAsync(OrderProduct order, CancellationToken cancellationToken = default)
	//{
	//	//await _orderRepository.AddAsync(order, cancellationToken);
	//	//_logger.LogInformation("Order created id: {Id}", order.Id);
	//}

	//private async Task ClearCartAsync(AccountCart cart, CancellationToken cancellationToken = default)
	//{
	//	//cart.Items.Clear();
	//	//await _cartRepository.UpdateOneAsync(
	//	//	cart.Id,
	//	//	update => update.Set(q => q.Items, cart.Items), cancellationToken: cancellationToken);
	//}

	//private async Task SendReceiptMail(ICollection<KeyValuePair<string, int>> itemsInCart,
	//	IReadOnlyDictionary<string, ProductClothe> productFromCartsDict, OrderProduct order)
	//{
	//	var sb = new StringBuilder();


	//	//var items = new List<ReceiptMailOrderItem>();


	//	//foreach (var (key, value) in itemsInCart)
	//	//{
	//	//	sb.Append(
	//	//		$"<tr>\r\n<td align=\"left\" width=\"75%\" style=\"padding: 6px 12px;font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\">" +
	//	//		$"{productFromCartsDict[key].Name} x {value}</td>\r\n<td align=\"left\" width=\"25%\" style=\"padding: 6px 12px;font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\">" +
	//	//		$"{productFromCartsDict[key].Price} vnd</td>\r\n</tr>");
	//	//}

	//	//var mail = new MailingTemplate(order.CustomerEmail, "Checkout Order", EmailTemplateNames.CheckoutOrderReceipt,
	//	//	new
	//	//	{
	//	//		OrderId = order.Id,
	//	//		ItemsHtmlContent = sb.ToString(),
	//	//		OrderAddress = order.DeliveryAddress,
	//	//		order.Total
	//	//	});

	//	//await _mailingService.SendEmailAsync(mail);
	//}
}

internal class ReceiptMailOrderItem
{
	public ReceiptMailOrderItem(string productName, int quantity, double price)
	{
		ProductName = productName;
		Quantity = quantity;
		Price = price;
	}

	public string ProductName { get; set; }
	public int Quantity { get; set; }
	public double Price { get; set; }
}
