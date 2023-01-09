using AutoMapper;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Categories.Commands.Edit;

public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;

	public EditCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_categoryRepository = categoryRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(EditCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var category = await _categoryRepository.GetByIntKey(request.Id, cancellationToken);

		_mapper.Map(request, category);
		_categoryRepository.Update(category);

		return await _unitOfWork.SaveChangesAsync(cancellationToken)
			? JsonApiResponse<EmptyUnitResponse>.Success()
			: JsonApiResponse<EmptyUnitResponse>.Fail();
	}
}
