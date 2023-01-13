using AutoMapper;
using Newtonsoft.Json;
using OnlineClothes.Application.Helpers;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Commands.CreateNewSku;

public sealed class
	CreateSkuCommandHandler : IRequestHandler<CreateSkuCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<CreateSkuCommandHandler> _logger;
	private readonly IMapper _mapper;
	private readonly ISkuRepository _skuRepository;
	private readonly StorageImageFileHelper _storageImageFileHelper;
	private readonly IUnitOfWork _unitOfWork;

	public CreateSkuCommandHandler(
		ILogger<CreateSkuCommandHandler> logger,
		IUnitOfWork unitOfWork,
		IMapper mapper,
		ISkuRepository skuRepository,
		StorageImageFileHelper storageImageFileHelper)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_skuRepository = skuRepository;
		_storageImageFileHelper = storageImageFileHelper;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(CreateSkuCommand request,
		CancellationToken cancellationToken)
	{
		if (await CheckExistedSku(request.Sku))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Sku đã tồn tại");
		}

		// begin tx
		await _unitOfWork.BeginTransactionAsync(cancellationToken);

		var productSku = _mapper.Map<CreateSkuCommand, ProductSku>(request);

		if (request.ImageFile is not null)
		{
			await _storageImageFileHelper.AddOrUpdateSkuImageAsync(productSku, request.ImageFile, cancellationToken);
		}

		await _skuRepository.AddAsync(productSku, cancellationToken: cancellationToken);

		if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		// commit
		await _unitOfWork.CommitAsync(cancellationToken);

		_logger.LogInformation("Create product: {model}", JsonConvert.SerializeObject(productSku));

		return JsonApiResponse<EmptyUnitResponse>.Created("Thêm sản phẩm thành công");
	}

	private async Task<bool> CheckExistedSku(string sku)
	{
		return await _skuRepository.AnyAsync(FilterBuilder<ProductSku>.Where(item => item.Sku == sku));
	}
}
