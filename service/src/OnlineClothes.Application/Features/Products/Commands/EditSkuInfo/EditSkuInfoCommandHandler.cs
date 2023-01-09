using AutoMapper;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Support.Builders.Predicate;

namespace OnlineClothes.Application.Features.Products.Commands.EditSkuInfo;

public class EditSkuInfoCommandHandler : IRequestHandler<EditSkuInfoCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IMapper _mapper;
	private readonly ISkuRepository _skuRepository;
	private readonly IUnitOfWork _unitOfWork;

	public EditSkuInfoCommandHandler(ISkuRepository skuRepository, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_skuRepository = skuRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(EditSkuInfoCommand request,
		CancellationToken cancellationToken)
	{
		if (!await CheckExistedSku(request.Sku))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		var sku = await _skuRepository.GetByStrKey(request.Sku, cancellationToken);

		_skuRepository.Update(sku);
		_mapper.Map(request, sku);

		return await _unitOfWork.SaveChangesAsync(cancellationToken)
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}

	private async Task<bool> CheckExistedSku(string sku)
	{
		return await _skuRepository.AnyAsync(FilterBuilder<ProductSku>.Where(x => x.Sku == sku));
	}
}
