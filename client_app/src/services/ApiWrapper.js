import axios from "axios";
import { TokenManage } from "./TokenManage";
import { toast } from "react-toastify";

export const BASE_API_DOMAIN_URL = "https://localhost:9443/api/v1";
const API_HEADER_AUTH_KEY = "Authorization";
const DEFAULT_HEADERS = {
  Accept: "application/json",
  "Content-Type": "application/json; charset=utf-8"
};

const injectToken = (config) => {
  try {
    if (TokenManage.checkAccessToken()) {
      if (config.headers !== undefined) {
        config.headers[
          API_HEADER_AUTH_KEY
        ] = `Bearer ${TokenManage.getAccessToken()}`;
      }
    }
    return config;
  } catch (error) {
    throw new Error(error);
  }
};

const handleResponse = (response) => {
  if (response.data.IsError || response.data.isError) {
    const msg = response.data?.Message ?? response.data?.message;
    console.log("axios response msg", msg);
  }

  return response;
};

const handleError = (error) => {
  console.log(error);
  const msg = error?.response?.data?.Message ?? error?.response?.data?.message;
  console.log("axios error msg", msg);
  return error.response;
};

const client = axios.create({
  baseURL: BASE_API_DOMAIN_URL,
  headers: DEFAULT_HEADERS,
  timeout: 3000
});

// Intercept request
client.interceptors.request.use(injectToken, (err) => {
  Promise.reject(err);
});

//  Handle response
client.interceptors.response.use(handleResponse, handleError);

/**
 * HTTP METHODS
 */

const post = async (url, payload) => {
  const result = await client.post(url, payload);
  if (result.status < 300 && result.status >= 200) {
    const msg = result.data?.message || result.data?.Message;
    if (msg) {
      toast.success(msg);
    }
  } else if (result.status >= 400) {
    const msg = result.data?.message || result.data?.Message;
    if (msg) {
      toast.error(msg);
    }
  }

  return result;
};

const put = async (url, payload) => {
  const result = await client.put(url, payload);

  if (result.status < 300 && result.status >= 200) {
    const msg = result.data?.message || result.data?.Message;
    if (msg) {
      toast.success(msg);
    }
  } else if (result.status >= 400) {
    const msg = result.data?.message || result.data?.Message;
    if (msg) {
      toast.error(msg);
    }
  }

  return result;
};

const get = async (url, config) => {
  const result = await client.get(url, config);
  return result.data;
};

const apiClient = {
  post,
  get,
  put
};

export { apiClient };
