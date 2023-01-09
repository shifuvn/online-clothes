using AutoMapper;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Persistence.Schemas.Products;

namespace OnlineClothes.Application.Features.Products.Commands.EditProductInfo;

public class EditProductCommandHandler : IRequestHandler<EditProductCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<EditProductCommandHandler> _logger;
	private readonly IMapper _mapper;
	private readonly IProductRepository _productRepository;
	private readonly IUnitOfWork _unitOfWork;

	public EditProductCommandHandler(
		ILogger<EditProductCommandHandler> logger,
		IProductRepository productRepository,
		IUnitOfWork unitOfWork, IMapper mapper)
	{
		_logger = logger;
		_productRepository = productRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(
		EditProductCommand request,
		CancellationToken cancellationToken)
	{
		// begin tx
		await _unitOfWork.BeginTransactionAsync(cancellationToken);

		await _productRepository.EditOneAsync(request.Id,
			_mapper.Map<EditProductCommand, PutProductInRepoObject>(request),
			cancellationToken);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);
		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		// commit tx
		await _unitOfWork.CommitAsync(cancellationToken);
		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
