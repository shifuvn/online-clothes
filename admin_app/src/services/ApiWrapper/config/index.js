import { processProductQueryParams } from "./productProvider";
import { BASE_API_DOMAIN_URI } from "../http-instances";

export const configUrl = (url, params) => {
  switch (url) {
    case "products":
      let uri = `${BASE_API_DOMAIN_URI}/${url}`;
      let query = processProductQueryParams(params);
      if (query !== undefined) {
        uri += `?${query}`;
      }
      return uri;

    default:
      return url;
  }
};

export const configResult = (url, result, action) => {
  switch (url) {
    case "products":
      switch (action) {
        case "get list":
          var data = {};
          data = result?.data?.data?.items?.map((item, idx) => ({
            id: item.id,
            ...item
          }));
          const total = result?.data?.data?.totalItems;
          return { data, total };

        default:
          return result;
      }

    default:
      return result;
  }
};
