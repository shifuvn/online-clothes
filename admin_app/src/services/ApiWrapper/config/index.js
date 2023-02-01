import { BASE_API_DOMAIN_URI } from "../http-instances";
import { RaHttpProviderAction } from "../http-provider-action";
import { handleSkuGetListResult } from "./skuProvider";

export const configUrl = (url, params, action) => {
  switch (url) {
    case "products":
    case "skus":
    case "productTypes":
    case "categories":
    case "brands":
      return handleDefaultConfigUrl(url, params, action);

    default:
      return url;
  }
};

export const configResult = (url, result, action, isFilterById = false) => {
  switch (url) {
    case "products":
    case "productTypes":
    case "categories":
    case "brands":
      switch (action) {
        case RaHttpProviderAction.getList:
          return handleDefaultConfigResultGetList(result, isFilterById);

        case RaHttpProviderAction.getOne:
          return handleDefaultConfigResultGetOne(result);

        default:
          return result;
      }
    case "skus":
      switch (action) {
        case RaHttpProviderAction.getList:
          return handleSkuGetListResult(result, isFilterById);

        case RaHttpProviderAction.getOne:
          let data = {};
          data = result?.data?.data ?? {};
          data.id = data.sku;
          return { data };

        default:
          return result;
      }

    default:
      return result;
  }
};

const handleDefaultConfigUrl = (url, params, action) => {
  console.log("url", url);
  console.log("params", params);

  let requestUrl = `${BASE_API_DOMAIN_URI}/${url}`;

  console.log("requestUrl", requestUrl);

  switch (action) {
    case RaHttpProviderAction.getOne:
      requestUrl += `/${params.id}`;
      break;

    case RaHttpProviderAction.getList:
      if (params.filter?.id !== undefined) {
        return requestUrl + `/${params.filter.id}`;
      }
      if (params.filter?.sku !== undefined) {
        return requestUrl + `/${params.filter.sku}`;
      }
      break;

    default:
      break;
  }

  if (params !== undefined && params) {
    let query = "?IncludeAll=true";
    const paging = params.pagination;
    if (paging !== undefined) {
      query += `&pageIndex=${paging.page}&pageSize=${paging.perPage}`;
    }

    const sort = params.sort;
    if (sort !== undefined) {
      query += `&orderBy=${sort.order}&sortBy=${sort.field}`;
    }

    const filter = params.filter;
    if (filter !== undefined) {
      query +=
        filter?.keyword === undefined ? "" : `&keyword=${filter.keyword}`;
    }

    requestUrl += query;
  }

  console.log("result url", requestUrl);
  return requestUrl;
};

/**
 * Handle getList result
 */
const handleDefaultConfigResultGetList = (result, isFilterById) => {
  let data = {};
  if (isFilterById) {
    data = result?.data.data === undefined ? [] : [result?.data?.data];
    return { data, total: data.length };
  } else {
    data = result?.data?.data?.items?.map((item, idx) => ({
      id: item.id,
      ...item
    }));
    const total = result?.data?.data?.totalItems;
    return { data, total };
  }
};

const handleDefaultConfigResultGetOne = (result) => {
  const data = result?.data.data ?? {};
  return { data };
};
