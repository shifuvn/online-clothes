﻿using System.ComponentModel;
using OnlineClothes.Application.Commons;
using OnlineClothes.Domain.Paging;

namespace OnlineClothes.Application.Features.Skus.Queries.Paging;

public class GetPagingSkuQuery : PagingRequest, IRequest<JsonApiResponse<PagingModel<ProductSkuBasicDto>>>
{
	public string? Keyword { get; set; } // key search

	[DefaultValue(QuerySortOrder.Descending)]
	public string? OrderBy { get; set; }

	public string? SortBy { get; set; }
	[DefaultValue(false)] public bool IncludeAll { get; set; }
}
