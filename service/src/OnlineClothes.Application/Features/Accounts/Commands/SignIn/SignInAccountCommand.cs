using FluentValidation;
using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignIn;

public sealed class SignInAccountCommand : IRequest<JsonApiResponse<SignInAccountCommandResult>>
{
	public string Email { get; init; } = null!;
	public string Password { get; init; } = null!;
}

internal sealed class SignInAccountCommandValidation : AbstractValidator<SignInAccountCommand>
{
	public SignInAccountCommandValidation()
	{
		RuleFor(q => q.Email)
			.EmailAddress().WithMessage("Email không hợp lệ")
			.NotEmpty().WithMessage("Email không được trống");

		RuleFor(q => q.Password)
			.NotEmpty().WithMessage("Mật khẩu không được trống");
	}
}
