using OnlineClothes.Application.Helpers;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Images.Commands.ReplaceSkuImage;

public class ReplaceSkuImageCommandHandler : IRequestHandler<ReplaceSkuImageCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ISkuRepository _skuRepository;
	private readonly StorageImageFileHelper _storageImageFileHelper;

	public ReplaceSkuImageCommandHandler(
		ISkuRepository skuRepository,
		StorageImageFileHelper storageImageFileHelper)
	{
		_skuRepository = skuRepository;
		_storageImageFileHelper = storageImageFileHelper;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(ReplaceSkuImageCommand request,
		CancellationToken cancellationToken)
	{
		var sku = await _skuRepository.GetSkuDetailBySkuAsync(request.Sku, cancellationToken);

		await _storageImageFileHelper.AddOrUpdateSkuImageAsync(sku, request.File, cancellationToken);

		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
