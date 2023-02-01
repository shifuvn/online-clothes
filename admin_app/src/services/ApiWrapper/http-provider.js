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

HttpApiProvider.delete = (url, params) => {
  const deleteUrl = url + `/${params.id}`;
  return http.instance.delete(deleteUrl);
};

HttpApiProvider.getOne = async (url, params) => {
  const configuredUrl = configUrl(url, params, RaHttpProviderAction.getOne);
  let result = await http.instance.get(configuredUrl);

  return configResult(url, result, RaHttpProviderAction.getOne);
};

HttpApiProvider.getList = async (url, params, options) => {
  const configuredUrl = configUrl(url, params, RaHttpProviderAction.getList);
  const result = await http.instance.get(configuredUrl, params, options);

  const isFilterById =
    params?.filter?.id !== undefined || params?.filter?.sku !== undefined;

  return configResult(url, result, RaHttpProviderAction.getList, isFilterById);
};

HttpApiProvider.create = async (url, payload) => {
  const configuredUrl = configUrl(url);
  configPayloadContentType(url);

  var result = await http.instance.post(configuredUrl, payload);
  return { data: { ...result } };
};

HttpApiProvider.createForm = async (url, payload) => {
  const configuredUrl = configUrl(url);
  http.instance.defaults.headers["Content-Type"] = "multipart/form-data";

  var result = await http.instance.post(configuredUrl, payload);
  return { data: { ...result } };
};

HttpApiProvider.update = async (url, payload) => {
  var configuredUrl = configUrl(url);
  var result = await http.instance.put(configuredUrl, payload);

  return { data: { ...result } };
};

HttpApiProvider.updateForm = async (url, payload) => {
  var configuredUrl = configUrl(url);
  http.instance.defaults.headers["Content-Type"] = "multipart/form-data";

  var result = await http.instance.put(configuredUrl, payload);

  return { data: { ...result } };
};

HttpApiProvider.deleteOne = async (url, params) => {
  console.log("deleteOne", url, params);
  var configuredUrl = configUrl(url, params);
  var result = await http.instance.delete(configuredUrl);
  return { data: { ...result } };
};

function configPayloadContentType(url) {
  if (url === "products" || url === "skus") {
    http.instance.defaults.headers["Content-Type"] = "multipart/form-data";
  }
}

export default HttpApiProvider;
