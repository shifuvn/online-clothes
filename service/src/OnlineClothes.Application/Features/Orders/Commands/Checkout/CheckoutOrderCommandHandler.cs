using Newtonsoft.Json;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Services.Mailing;
using OnlineClothes.Application.Services.UserContext;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Support.Utilities.Extensions;

namespace OnlineClothes.Application.Features.Orders.Commands.Checkout;

public class
	CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private const string EmptyCartError = "Giỏ hàng trống";
	private readonly ICartRepository _cartRepository;
	private readonly ILogger<CheckoutOrderCommandHandler> _logger;
	private readonly IMailingService _mailingService;
	private readonly IOrderRepository _orderRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IUserContext _userContext;


	public CheckoutOrderCommandHandler(
		IUserContext userContext,
		ILogger<CheckoutOrderCommandHandler> logger,
		IOrderRepository orderRepository,
		ICartRepository cartRepository,
		IMailingService mailingService,
		IUnitOfWork unitOfWork)
	{
		_userContext = userContext;
		_logger = logger;
		_orderRepository = orderRepository;
		_cartRepository = cartRepository;
		_mailingService = mailingService;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(CheckoutOrderCommand request,
		CancellationToken cancellationToken)
	{
		var cart = await _cartRepository.GetCurrentCart();

		if (cart.IsEmpty())
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail(EmptyCartError);
		}

		// Check is every sku still available, if not return FAIL
		var storeSku = GetStoreSkuFromCart(cart);
		foreach (var productSku in storeSku.Where(productSku => !productSku.IsAvailable()))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail($"Sku {productSku.Sku} không còn được bán");
		}

		var skuQuantityDict = FlattenCartItemToSkuQuantityDict(cart);
		var storeSkuQuantityDict = FlattenStoreSkuToDictionary(storeSku);

		var (validNumber, validNumberMessage) = CheckValidNumberInStock(skuQuantityDict, storeSkuQuantityDict);
		if (!validNumber)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail(validNumberMessage);
		}

		await _unitOfWork.BeginTransactionAsync(cancellationToken);

		var order = await ProcessNewOrder(request, cancellationToken, cart);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Có lỗi, vui lòng thử lại");
		}

		await _unitOfWork.CommitAsync(cancellationToken);

		_logger.LogInformation("Create order: {object}", JsonConvert.SerializeObject(order));

		return JsonApiResponse<EmptyUnitResponse>.Created();
	}

	private async Task<Order> ProcessNewOrder(CheckoutOrderCommand request, CancellationToken cancellationToken,
		AccountCart cart)
	{
		var order = InitializeNewOrder(request, cart);
		await _orderRepository.AddAsync(order, cancellationToken: cancellationToken);
		_cartRepository.Update(cart);
		cart.Clear();
		return order;
	}

	private Order InitializeNewOrder(CheckoutOrderCommand request, AccountCart cart)
	{
		var orderItems = cart.Items.Select(cartItem => new OrderItem
		{
			OrderId = 0, ProductSkuId = cartItem.ProductSkuId, Price = cartItem.ProductSku.GetPrice(),
			Quantity = cartItem.Quantity
		}).ToList();


		var order = new Order(
			_userContext.GetNameIdentifier(),
			cart.TotalPaid(),
			request.Address,
			request.Note,
			OrderState.Pending,
			false,
			orderItems);
		return order;
	}

	/// <summary>
	/// Check if number of item in cart is not HIGHER than inStock
	/// </summary>
	/// <param name="skuQuantityDict"></param>
	/// <param name="storeSkuQuantityDict"></param>
	/// <returns></returns>
	private (bool Valid, string? Message) CheckValidNumberInStock(
		IReadOnlyDictionary<string, int> skuQuantityDict,
		IReadOnlyDictionary<string, int> storeSkuQuantityDict)
	{
		foreach (var (cartSku, cartQuantity) in skuQuantityDict)
		{
			if (!storeSkuQuantityDict.TryGetValue(cartSku, out var skuInStock)) continue;

			if (cartQuantity > skuInStock)
			{
				return (false, $"Sku {cartSku} chỉ còn {skuInStock} sản phẩm");
			}
		}

		return (true, null);
	}

	/// <summary>
	/// Select list ProductSku => Dictionary(sku, inStock)
	/// </summary>
	/// <param name="storeSku"></param>
	/// <returns></returns>
	private static Dictionary<string, int> FlattenStoreSkuToDictionary(List<ProductSku> storeSku)
	{
		var storeSkuQuantityDict = storeSku
			.ToDictionary(item => item.Sku, item => item.InStock);
		return storeSkuQuantityDict;
	}

	/// <summary>
	/// Select Cart to list ProductSku entity
	/// </summary>
	/// <param name="cart"></param>
	/// <returns></returns>
	private static List<ProductSku> GetStoreSkuFromCart(AccountCart cart)
	{
		var storeSku = cart.Items
			.SelectList(q => q.ProductSku);
		return storeSku;
	}

	/// <summary>
	/// Select Cart.Item to dictionary(sku, quantity)
	/// </summary>
	/// <param name="cart"></param>
	/// <returns></returns>
	private static Dictionary<string, int> FlattenCartItemToSkuQuantityDict(AccountCart cart)
	{
		var skuQuantityDict = cart.Items
			.ToDictionary(cartItem => cartItem.ProductSkuId, cartItem => cartItem.Quantity);
		return skuQuantityDict;
	}
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
