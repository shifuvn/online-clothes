using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Product.Commands.UploadImage;

public class UploadProductImageCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public UploadProductImageCommand(string productId, IFormFile file)
	{
		ProductId = productId;
		File = file;
	}

	public string ProductId { get; init; }
	public IFormFile File { get; init; }
}

public sealed class UploadProductImageCommandValidation : AbstractValidator<UploadProductImageCommand>
{
	public UploadProductImageCommandValidation()
	{
		RuleFor(q => q.ProductId)
			.NotEmpty().WithMessage("Mã sản phẩm bị trống");
	}
}
