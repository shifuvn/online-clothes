using FluentValidation;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignIn;

public sealed class SignInCommand : IRequest<JsonApiResponse<SignInCommandResult>>
{
	public string Email { get; init; } = null!;
	public string Password { get; init; } = null!;
}

internal sealed class SignInCommandValidation : AbstractValidator<SignInCommand>
{
	public SignInCommandValidation()
	{
		RuleFor(q => q.Email)
			.EmailAddress().WithMessage("Email không hợp lệ")
			.NotEmpty().WithMessage("Email không được trống");

		RuleFor(q => q.Password)
			.NotEmpty().WithMessage("Mật khẩu không được trống");
	}
}
