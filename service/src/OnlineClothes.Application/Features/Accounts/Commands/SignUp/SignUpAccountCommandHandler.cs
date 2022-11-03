using MediatR;
using Microsoft.Extensions.Logging;
using OnlineClothes.Domain.Common;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Accounts.Commands.SignUp;

internal sealed class
	SignUpAccountCommandHandler : IRequestHandler<SignUpAccountCommand, JsonApiResponse<EmptyUnitResponse>>
{
	private readonly ILogger<SignUpAccountCommandHandler> _logger;

	public SignUpAccountCommandHandler(ILogger<SignUpAccountCommandHandler> logger)
	{
		_logger = logger;
	}

	public Task<JsonApiResponse<EmptyUnitResponse>> Handle(SignUpAccountCommand request,
		CancellationToken cancellationToken)
	{
		var newAccount = UserAccount.Create(request.Email, request.Password, UserAccountRole.Client);

		_logger.LogInformation($"Create new account: {newAccount.Email}");

		return Task.FromResult(JsonApiResponse<EmptyUnitResponse>.Success());
	}
}
