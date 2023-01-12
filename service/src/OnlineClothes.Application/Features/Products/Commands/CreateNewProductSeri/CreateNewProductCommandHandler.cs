using AutoMapper;
using Newtonsoft.Json;
using OnlineClothes.Application.Helpers;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Commands.CreateNewProductSeri;

public class
	CreateNewProductCommandHandler : IRequestHandler<CreateNewProductCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<CreateNewProductCommandHandler> _logger;
	private readonly IMapper _mapper;
	private readonly IProductRepository _productRepository;
	private readonly ISkuRepository _skuRepository;
	private readonly StorageImageFileHelper _storageImageFileHelper;
	private readonly IUnitOfWork _unitOfWork;

	public CreateNewProductCommandHandler(
		ILogger<CreateNewProductCommandHandler> logger,
		IProductRepository productRepository,
		IUnitOfWork unitOfWork,
		IMapper mapper,
		ISkuRepository skuRepository,
		StorageImageFileHelper storageImageFileHelper)
	{
		_logger = logger;
		_productRepository = productRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_skuRepository = skuRepository;
		_storageImageFileHelper = storageImageFileHelper;
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

		if (request.ImageFile is not null)
		{
			await _storageImageFileHelper.AddOrUpdateSkuImageAsync(product.ProductSkus.First(), request.ImageFile,
				cancellationToken);
		}

		// MAGIC code (used to assign image to first sku created)
		product.ThumbnailImage = product.ProductSkus.First().Image;

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
