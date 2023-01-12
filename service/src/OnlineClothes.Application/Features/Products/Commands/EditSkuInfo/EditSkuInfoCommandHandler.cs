using AutoMapper;
using OnlineClothes.Application.Helpers;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Support.Builders.Predicate;

namespace OnlineClothes.Application.Features.Products.Commands.EditSkuInfo;

public class EditSkuInfoCommandHandler : IRequestHandler<EditSkuInfoCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IImageRepository _imageRepository;
	private readonly IMapper _mapper;
	private readonly ISkuRepository _skuRepository;
	private readonly StorageImageFileHelper _storageImageFileHelper;
	private readonly IUnitOfWork _unitOfWork;

	public EditSkuInfoCommandHandler(
		ISkuRepository skuRepository,
		IUnitOfWork unitOfWork,
		IMapper mapper,
		IImageRepository imageRepository,
		StorageImageFileHelper storageImageFileHelper)
	{
		_skuRepository = skuRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_imageRepository = imageRepository;
		_storageImageFileHelper = storageImageFileHelper;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(EditSkuInfoCommand request,
		CancellationToken cancellationToken)
	{
		if (!await CheckExistedSku(request.Sku))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		// begin tx
		await _unitOfWork.BeginTransactionAsync(cancellationToken);

		var sku = await _skuRepository.GetByStrKey(request.Sku, cancellationToken);

		_skuRepository.Update(sku);
		_mapper.Map(request, sku);

		//await _storageImageFileHelper.ReplaceImageFile(sku, request.ImageFile);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);
		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		// commit tx
		await _unitOfWork.CommitAsync(cancellationToken);

		return JsonApiResponse<EmptyUnitResponse>.Success();
	}

	private async Task<bool> CheckExistedSku(string sku)
	{
		return await _skuRepository.AnyAsync(FilterBuilder<ProductSku>.Where(x => x.Sku == sku));
	}
}
