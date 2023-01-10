using AutoMapper;
using Newtonsoft.Json;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Commands.CreateNewProductSeri;

public class
	CreateNewProductCommandHandler : IRequestHandler<CreateNewProductCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IImageRepository _imageRepository;
	private readonly ILogger<CreateNewProductCommandHandler> _logger;
	private readonly IMapper _mapper;
	private readonly IProductRepository _productRepository;
	private readonly ISkuRepository _skuRepository;
	private readonly IUnitOfWork _unitOfWork;

	public CreateNewProductCommandHandler(
		ILogger<CreateNewProductCommandHandler> logger,
		IProductRepository productRepository,
		IUnitOfWork unitOfWork,
		IMapper mapper,
		ISkuRepository skuRepository,
		IImageRepository imageRepository)
	{
		_logger = logger;
		_productRepository = productRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_skuRepository = skuRepository;
		_imageRepository = imageRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(CreateNewProductCommand request,
		CancellationToken cancellationToken)
	{
		if (await CheckExistedSku(request.Sku))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Sku đã tồn tại");
		}

		// begin tx
		await _unitOfWork.BeginTransactionAsync(cancellationToken);

		var product = _mapper.Map<CreateNewProductCommand, Product>(request);

		product.ThumbnailImage =
			await _imageRepository.AddProductImageFileAsync(request.ImageFile, request.Sku, cancellationToken);

		// MAGIC code (used to assign image to first sku created)
		product.ProductSkus.First().Image = product.ThumbnailImage;

		await _productRepository.AddAsync(product, cancellationToken: cancellationToken);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);
		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		// commit tx
		await _unitOfWork.CommitAsync(cancellationToken);

		_logger.LogInformation("Create new product seri: {object}", JsonConvert.SerializeObject(product));

		return JsonApiResponse<EmptyUnitResponse>.Created("Tạo dòng sản phẩm thành công");
	}

	private async Task<bool> CheckExistedSku(string sku)
	{
		var entry = await _skuRepository.FindOneAsync(productSku => productSku.Sku.Equals(sku));

		return entry is not null;
	}
}
