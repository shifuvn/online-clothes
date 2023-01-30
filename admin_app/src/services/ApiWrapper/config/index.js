import { processProductQueryParams } from "./productProvider";
import { processSkyQueryParams } from "./skuProvider";
import { BASE_API_DOMAIN_URI } from "../http-instances";

export const configUrl = (url, params, action) => {
  let requestUrl = `${BASE_API_DOMAIN_URI}/${url}`;
  if (action === "getOne") {
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
  let data = {};
  switch (url) {
    case "products":
      switch (action) {
        case "getList":
          data = result?.data?.data?.items?.map((item, idx) => ({
            id: item.id,
            ...item
          }));
          const total = result?.data?.data?.totalItems;
          return { data, total };

        default:
          return result;
      }
    case "skus":
      switch (action) {
        case "getList":
          data = result?.data?.data?.items?.map((item, idx) => ({
            id: item.id,
            ...item
          }));
          const total = result?.data?.data?.totalItems;
          return { data, total };

        case "getOne":
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
