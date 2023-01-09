//using OnlineClothes.Infrastructure.Repositories.Abstracts;

using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Carts.Commands.AddItem;

public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private const string ProductNotAvailableError = "Sản phẩm đang không bày bán";
	private const string ProductInStockNotEnoughError = "Số lượng tồn kho không đủ";

	private readonly ICartRepository _cartRepository;
	private readonly IProductRepository _productRepository;
	private readonly ISkuRepository _skuRepository;
	private readonly IUnitOfWork _unitOfWork;

	public AddCartItemCommandHandler(
		ICartRepository cartRepository,
		IProductRepository productRepository,
		IUnitOfWork unitOfWork,
		ISkuRepository skuRepository)
	{
		_cartRepository = cartRepository;
		_productRepository = productRepository;
		_unitOfWork = unitOfWork;
		_skuRepository = skuRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(AddCartItemCommand request,
		CancellationToken cancellationToken)
	{
		var productSku = await _skuRepository.GetSkuDetailBySkuAsync(request.ProductSku, cancellationToken);

		if (!productSku.Product.IsAvailable())
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail(ProductNotAvailableError);
		}

		if (productSku.InStock < request.Quantity)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail(ProductInStockNotEnoughError);
		}

		var cart = await _cartRepository.GetCurrentCart();

		_cartRepository.Update(cart);
		cart.UpdateItemQuantity(request.ProductSku, request.Quantity);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);
		return !save ? JsonApiResponse<EmptyUnitResponse>.Fail() : JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
