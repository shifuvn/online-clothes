using Microsoft.AspNetCore.Http;

namespace OnlineClothes.Application.Features.Profile.Commands.EditAvatar;

public sealed class EditAvatarCommand : IRequest<JsonApiResponse<EmptyUnitResponse>>
{
	public IFormFile Avatar { get; init; } = null!;
}
