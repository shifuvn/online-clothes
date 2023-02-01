import { BASE_API_DOMAIN_URI } from "../http-instances";
import { RaHttpProviderAction } from "../http-provider-action";
import { handleSkuGetListResult } from "./skuProvider";

export const configUrl = (url, params, action) => {
  let requestUrl = `${BASE_API_DOMAIN_URI}/${url}`;
  if (action === RaHttpProviderAction.getOne) {
    requestUrl += `/${params.id}`;
  }
  console.log("requestUrl", requestUrl);
  let query;
  switch (url) {
    case "products":
    case "skus":
    case "productTypes":
    case "categories":
    case "brands":
      query = handleDefaultConfigUrl(params);
      if (query) {
        requestUrl += `?${query}`;
      }
      return requestUrl;

    default:
      return url;
  }
};

export const configResult = (url, result, action) => {
  switch (url) {
    case "products":
    case "productTypes":
    case "categories":
    case "brands":
      switch (action) {
        case RaHttpProviderAction.getList:
          return handleDefaultConfigResultGetList(result);

        case RaHttpProviderAction.getOne:
          return handleDefaultConfigResultGetOne(result);

        default:
          return result;
      }
    case "skus":
      switch (action) {
        case RaHttpProviderAction.getList:
          return handleSkuGetListResult(result);

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

const handleDefaultConfigUrl = (params) => {
  if (!params || params === undefined) {
    return undefined;
  }

  let query = "IncludeAll=true&";
  const paging = params.pagination;
  if (paging !== undefined) {
    query += `pageIndex=${paging.page}&pageSize=${paging.perPage}`;
  }

  const sort = params.sort;
  if (sort !== undefined) {
    query += `&orderBy=${sort.order}&sortBy=${sort.field}`;
  }
  return query;
};

/**
 * Handle getList result
 */
const handleDefaultConfigResultGetList = (result) => {
  const data = result?.data?.data?.items?.map((item, idx) => ({
    id: item.id,
    ...item
  }));
  const total = result?.data?.data?.totalItems;
  return { data, total };
};

const handleDefaultConfigResultGetOne = (result) => {
  const data = result?.data.data ?? {};
  return { data };
};
