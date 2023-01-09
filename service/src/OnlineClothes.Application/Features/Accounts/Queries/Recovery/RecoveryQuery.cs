using FluentValidation;

namespace OnlineClothes.Application.Features.Accounts.Queries.Recovery;

public sealed class RecoveryQuery : IRequest<JsonApiResponse<RecoveryQueryResult>>
{
	public string Token { get; set; } = null!;
}

public sealed class RecoveryQueryValidation : AbstractValidator<RecoveryQuery>
{
	public RecoveryQueryValidation()
	{
		RuleFor(q => q.Token)
			.NotEmpty();
	}
}
