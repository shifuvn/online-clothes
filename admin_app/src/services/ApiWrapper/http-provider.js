import { HttpInstance } from "./http-instances";
import { configResult, configUrl } from "./config";

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

HttpApiProvider.getList = async (url, params, options) => {
  const configuredUrl = configUrl(url, params);
  const result = await http.instance.get(configuredUrl, params, options);
  return configResult(url, result, "get list");
};

HttpApiProvider.create = async (url, payload) => {
  const configuredUrl = configUrl(url);
  http.instance.defaults.headers["Content-Type"] = "multipart/form-data";
  console.log("payload", payload);
  var result = await http.instance.post(configuredUrl, payload);
  console.log("result", result);
  return { data: { ...payload } };
};

export default HttpApiProvider;
