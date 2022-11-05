using FluentValidation;
using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.Activate;

public sealed class ActivateCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public string Token { get; init; } = null!;
}

internal sealed class ActivateCommandValidation : AbstractValidator<ActivateCommand>
{
	public ActivateCommandValidation()
	{
		RuleFor(q => q.Token)
			.NotEmpty().WithMessage("Không hợp lệ");
	}
}
