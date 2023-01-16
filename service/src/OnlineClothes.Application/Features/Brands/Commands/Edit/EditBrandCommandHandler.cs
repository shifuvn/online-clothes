using AutoMapper;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Brands.Commands.Edit;

public class EditBrandCommandHandler : IRequestHandler<EditBrandCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IBrandRepository _brandRepository;
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;

	public EditBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_brandRepository = brandRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(EditBrandCommand request,
		CancellationToken cancellationToken)
	{
		var brand = await _brandRepository.GetByIntKey(request.Id, cancellationToken);

		_mapper.Map(request, brand);

		_brandRepository.Update(brand);

		return await _unitOfWork.SaveChangesAsync(cancellationToken)
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
