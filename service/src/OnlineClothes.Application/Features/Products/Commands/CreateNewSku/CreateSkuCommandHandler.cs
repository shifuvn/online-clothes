using AutoMapper;
using Newtonsoft.Json;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Products.Commands.CreateNewSku;

public sealed class
	CreateSkuCommandHandler : IRequestHandler<CreateSkuCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<CreateSkuCommandHandler> _logger;
	private readonly IMapper _mapper;
	private readonly ISkuRepository _skuRepository;
	private readonly IUnitOfWork _unitOfWork;

	public CreateSkuCommandHandler(
		ILogger<CreateSkuCommandHandler> logger,
		IUnitOfWork unitOfWork,
		IMapper mapper,
		ISkuRepository skuRepository)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_skuRepository = skuRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(CreateSkuCommand request,
		CancellationToken cancellationToken)
	{
		var productSku = _mapper.Map<CreateSkuCommand, ProductSku>(request);
		await _skuRepository.AddAsync(productSku, cancellationToken: cancellationToken);

		if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		_logger.LogInformation("Create product: {model}", JsonConvert.SerializeObject(productSku));

		return JsonApiResponse<EmptyUnitResponse>.Created("Thêm sản phẩm thành công");
	}
}
