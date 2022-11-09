using MediatR;
using OnlineClothes.Infrastructure.Repositories.Abstracts;
using OnlineClothes.Support.HttpResponse;

namespace OnlineClothes.Application.Features.Cart.Queries.GetInfo;

public class GetCartInfoQueryHandler : IRequestHandler<GetCartInfoQuery, JsonApiResponse<GetCartInfoQueryViewModel>>
{
	private readonly ICartRepository _cartRepository;

	public GetCartInfoQueryHandler(ICartRepository cartRepository)
	{
		_cartRepository = cartRepository;
	}

	public async Task<JsonApiResponse<GetCartInfoQueryViewModel>> Handle(GetCartInfoQuery request,
		CancellationToken cancellationToken)
	{
		var cartInfo = await _cartRepository.GetItems(cancellationToken);

		var data = cartInfo.Items.Select(GetCartInfoQueryViewModel.Item.Create).ToList();

		var viewModel = new GetCartInfoQueryViewModel(data);

		return JsonApiResponse<GetCartInfoQueryViewModel>.Success(data: viewModel);
	}
}
