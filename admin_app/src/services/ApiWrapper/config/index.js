import { processProductQueryParams } from "./productProvider";
import { processSkyQueryParams } from "./skuProvider";
import { BASE_API_DOMAIN_URI } from "../http-instances";
import { RaHttpProviderAction } from "../http-provider-action";

export const configUrl = (url, params, action) => {
  let requestUrl = `${BASE_API_DOMAIN_URI}/${url}`;
  if (action === RaHttpProviderAction.getOne) {
    requestUrl += `/${params.id}`;
  }
  console.log("requestUrl", requestUrl);
  let query;
  switch (url) {
    case "products":
      query = processProductQueryParams(params);
      if (query !== undefined) {
        requestUrl += `?${query}`;
      }
      return requestUrl;

    case "skus":
      query = processSkyQueryParams(params);
      if (query !== undefined) {
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
      switch (action) {
        case RaHttpProviderAction.getList:
          return handleConfigResultGetList(result);

        case RaHttpProviderAction.getOne:
          let data = {};
          data = result?.data.data ?? {};
          return data;

        default:
          return result;
      }
    case "skus":
      switch (action) {
        case RaHttpProviderAction.getList:
          return handleConfigResultGetList(result);

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

/**
 * Handle getList result
 */
const handleConfigResultGetList = (result) => {
  const data = result?.data?.data?.items?.map((item, idx) => ({
    id: item.id,
    ...item
  }));
  const total = result?.data?.data?.totalItems;
  return { data, total };
};
