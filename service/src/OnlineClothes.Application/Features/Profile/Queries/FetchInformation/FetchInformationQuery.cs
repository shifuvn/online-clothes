using MediatR;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Profile.Queries.FetchInformation;

public sealed class FetchInformationQuery : IRequest<JsonApiResponse<FetchInformationQueryResult>>
{
}
