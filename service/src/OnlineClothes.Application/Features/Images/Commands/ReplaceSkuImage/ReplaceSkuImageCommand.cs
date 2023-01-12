using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace OnlineClothes.Application.Features.Images.Commands.ReplaceSkuImage;

public class ReplaceSkuImageCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Sku { get; set; } = null!;
	public IFormFile File { get; set; } = null!;
}

public sealed class ReplaceSkuImageCommandValidation : AbstractValidator<ReplaceSkuImageCommand>
{
	public ReplaceSkuImageCommandValidation()
	{
		RuleFor(q => q.Sku)
			.NotEmpty();
		RuleFor(q => q.File)
			.NotNull();
	}
}
