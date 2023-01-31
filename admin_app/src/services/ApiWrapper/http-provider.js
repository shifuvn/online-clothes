import { HttpInstance } from "./http-instances";
import { configResult, configUrl } from "./config";
import { RaHttpProviderAction } from "./http-provider-action";

const http = new HttpInstance();

const HttpApiProvider = {};

HttpApiProvider.get = (url, params, options) => {
  return http.instance.get(url, { params: params, ...options });
};

HttpApiProvider.post = (url, params, options) => {
  return http.instance.post(url, params, options);
};

HttpApiProvider.put = (url, params, options) => {
  return http.instance.put(url, params, options);
};

HttpApiProvider.delete = (url, options) => {
  return http.instance.delete(url, options);
};

HttpApiProvider.getOne = async (url, params) => {
  const configuredUrl = configUrl(url, params, RaHttpProviderAction.getOne);
  let result = await http.instance.get(configuredUrl);

  return configResult(url, result, RaHttpProviderAction.getOne);
};

HttpApiProvider.getList = async (url, params, options) => {
  const configuredUrl = configUrl(url, params, RaHttpProviderAction.getList);
  const result = await http.instance.get(configuredUrl, params, options);
  return configResult(url, result, RaHttpProviderAction.getList);
};

HttpApiProvider.create = async (url, payload) => {
  const configuredUrl = configUrl(url);
  configPayloadContentTypeCreateAction(url);

  var result = await http.instance.post(configuredUrl, payload);
  return { data: { ...result } };
};

HttpApiProvider.update = async (url, payload) => {
  var configuredUrl = configUrl(url);
  var result = await http.instance.put(configuredUrl, payload);

  return { data: { ...result } };
};

function configPayloadContentTypeCreateAction(url) {
  if (url === "products" || url === "skus") {
    http.instance.defaults.headers["Content-Type"] = "multipart/form-data";
  }
}

export default HttpApiProvider;
