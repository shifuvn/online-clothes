using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Profile.Commands.EditAvatar;

public sealed class EditAvatarCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public IFormFile Avatar { get; init; } = null!;
}
