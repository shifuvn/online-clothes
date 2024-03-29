﻿using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Categories.Commands.Create;

public sealed class
	CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly ILogger<CreateCategoryCommandHandler> _logger;
	private readonly IUnitOfWork _unitOfWork;

	public CreateCategoryCommandHandler(ILogger<CreateCategoryCommandHandler> logger,
		ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
	{
		_logger = logger;
		_categoryRepository = categoryRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(CreateCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var category = new Category(request.Name, request.Description);
		await _categoryRepository.AddAsync(category, cancellationToken: cancellationToken);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);

		return !save ? JsonApiResponse<EmptyUnitResponse>.Fail() : JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
