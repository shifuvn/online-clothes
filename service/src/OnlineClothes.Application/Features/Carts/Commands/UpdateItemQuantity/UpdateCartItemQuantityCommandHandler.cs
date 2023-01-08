using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Carts.Commands.UpdateItemQuantity;

public class
	UpdateCartItemQuantityCommandHandler : IRequestHandler<UpdateCartItemQuantityCommand,
		JsonApiResponse<EmptyUnitResponse>>
{
	private const string ProductNotAvailableError = "Sản phẩm đang không bày bán";
	private const string ProductInStockNotEnoughError = "Số lượng tồn kho không đủ";

	private readonly ICartRepository _cartRepository;
	private readonly IProductRepository _productRepository;
	private readonly ISkuRepository _skuRepository;
	private readonly IUnitOfWork _unitOfWork;

	public UpdateCartItemQuantityCommandHandler(ICartRepository cartRepository, IProductRepository productRepository,
		ISkuRepository skuRepository, IUnitOfWork unitOfWork)
	{
		_cartRepository = cartRepository;
		_productRepository = productRepository;
		_skuRepository = skuRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(UpdateCartItemQuantityCommand request,
		CancellationToken cancellationToken)
	{
		var productSku = await _skuRepository.GetSkuDetailBySkuAsync(request.Sku, cancellationToken);

		if (!productSku.Product.IsAvailable())
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail(ProductNotAvailableError);
		}

		if (productSku.InStock < request.Number)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail(ProductInStockNotEnoughError);
		}

		var cart = await _cartRepository.GetCurrentCart();

		_cartRepository.Update(cart);
		cart.UpdateItemQuantity(request.Sku, request.Number);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		return !save
			? JsonApiResponse<EmptyUnitResponse>.Fail()
			: JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
