using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Support.Exceptions.HttpExceptionCodes;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Commands.NewProduct;

public sealed class
	CreateNewClotheCommandHandler : IRequestHandler<CreateNewClotheCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<CreateNewClotheCommandHandler> _logger;
	private readonly IProductRepository _productRepository;

	public CreateNewClotheCommandHandler(ILogger<CreateNewClotheCommandHandler> logger,
		IProductRepository productRepository)
	{
		_logger = logger;
		_productRepository = productRepository;
	}

	public async Task<JsonApiResponse<EmptyUnitResponse>> Handle(CreateNewClotheCommand request,
		CancellationToken cancellationToken)
	{
		var newProduct = ProductClothe.Create(
			request.Name,
			request.Description,
			request.Price,
			request.Stock,
			request.Categories,
			request.Tags,
			new ProductClothe.ClotheDetail(request.Sizes, request.Materials, request.Type));

		try
		{
			await _productRepository.InsertAsync(newProduct, cancellationToken);
			return JsonApiResponse<EmptyUnitResponse>.Success(StatusCodes.Status201Created, "Thêm sản phẩm thành công");
		}
		catch (Exception e)
		{
			_logger.LogCritical(e, "Fail to create new product");
			throw new InternalServerErrorException(e.Message);
		}
	}
}
