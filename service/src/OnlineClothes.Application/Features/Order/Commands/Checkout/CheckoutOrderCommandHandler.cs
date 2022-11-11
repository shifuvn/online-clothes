using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Infrastructure.Services.UserContext.Abstracts;
using OnlineClothes.Support.Builders.Predicate;
using OnlineClothes.Support.Exceptions;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Order.Commands.Checkout;

public class
	CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, JsonApiResponse<CheckoutOrderCommandViewModel>>
{
	private readonly IAccountRepository _accountRepository;
	private readonly ICartRepository _cartRepository;
	private readonly ILogger<CheckoutOrderCommandHandler> _logger;
	private readonly IOrderRepository _orderRepository;
	private readonly IProductRepository _productRepository;
	private readonly IUserContext _userContext;


	public CheckoutOrderCommandHandler(IUserContext userContext, ILogger<CheckoutOrderCommandHandler> logger,
		IProductRepository productRepository, IOrderRepository orderRepository, ICartRepository cartRepository,
		IAccountRepository accountRepository)
	{
		_userContext = userContext;
		_logger = logger;
		_productRepository = productRepository;
		_orderRepository = orderRepository;
		_cartRepository = cartRepository;
		_accountRepository = accountRepository;
	}

	public async Task<JsonApiResponse<CheckoutOrderCommandViewModel>> Handle(CheckoutOrderCommand request,
		CancellationToken cancellationToken)
	{
		var accountCart = await _cartRepository.FindOneAsync(
			FilterBuilder<AccountCart>.Where(q => q.AccountId == _userContext.GetNameIdentifier()), cancellationToken);
		NullValueReferenceException.ThrowIfNull(accountCart, nameof(accountCart));

		if (!accountCart.Items.Any())
		{
			return JsonApiResponse<CheckoutOrderCommandViewModel>.Fail("Empty cart");
		}

		var itemsInCart = accountCart.Items.Select(q => new KeyValuePair<string, int>(q.ProductId, q.Quantity))
			.ToArray();
		var itemInCardIds = itemsInCart.Select(x => x.Key).ToHashSet();

		var productsFromCart = (await _productRepository.FindAsync(
				FilterBuilder<ProductClothe>.Where(q => itemInCardIds.Contains(q.Id)),
				cancellationToken: cancellationToken))
			.ToList()
			.ToDictionary(q => q.Id);

		foreach (var (key, value) in itemsInCart)
		{
			if (value > productsFromCart[key].Stock)
			{
				return JsonApiResponse<CheckoutOrderCommandViewModel>.Fail(data: new CheckoutOrderCommandViewModel(key),
					message: $"Sản phẩm {productsFromCart[key].Name} không đủ");
			}
		}

		var account = await _accountRepository.GetOneAsync(_userContext.GetNameIdentifier(), cancellationToken);
		var order = new OrderProduct(account.Id, account.Email, request.Address);
		foreach (var (key, value) in itemsInCart)
		{
			order.Add(new OrderProduct.OrderItem(key, value, productsFromCart[key].Price));
		}

		await Task.WhenAll(ClearCartAsync(accountCart, cancellationToken),
			CreateOrderTransactionAsync(order, cancellationToken));

		return JsonApiResponse<CheckoutOrderCommandViewModel>.Success();
	}

	private async Task CreateOrderTransactionAsync(OrderProduct order, CancellationToken cancellationToken = default)
	{
		await _orderRepository.InsertAsync(order, cancellationToken);
		_logger.LogInformation("Order created id: {Id}", order.Id);
	}

	private async Task ClearCartAsync(AccountCart cart, CancellationToken cancellationToken = default)
	{
		cart.Items.Clear();
		await _cartRepository.UpdateOneAsync(
			cart.Id,
			update => update.Set(q => q.Items, cart.Items), cancellationToken: cancellationToken);
	}
}
