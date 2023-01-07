using AutoMapper;
using Newtonsoft.Json;
using OnlineClothes.Application.Persistence;

namespace OnlineClothes.Application.Features.Brands.Commands.Create;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly IBrandRepository _brandRepository;
	private readonly ILogger<CreateBrandCommandHandler> _logger;
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;

	public CreateBrandCommandHandler(IUnitOfWork unitOfWork,
		IBrandRepository brandRepository,
		ILogger<CreateBrandCommandHandler> logger,
		IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_brandRepository = brandRepository;
		_logger = logger;
		_mapper = mapper;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(CreateBrandCommand request,
		CancellationToken cancellationToken)
	{
		if (await _brandRepository.IsNameExistedAsync(request.Name, cancellationToken))
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail("Brand đã tồn tại");
		}

		var brand = _mapper.Map<Brand>(request);
		await _brandRepository.AddAsync(brand, cancellationToken: cancellationToken);

		var save = await _unitOfWork.SaveChangesAsync(cancellationToken);
		if (!save)
		{
			return JsonApiResponse<EmptyUnitResponse>.Fail();
		}

		_logger.LogInformation("Create brand: {object}", JsonConvert.SerializeObject(brand));
		return JsonApiResponse<EmptyUnitResponse>.Success();
	}
}
