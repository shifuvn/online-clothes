import axios from "axios";
import token from "../token";
import { toast } from "react-toastify";

export const BASE_API_DOMAIN_URI = process.env.REACT_APP_API_DOMAIN;
const API_HEADER_AUTH_KEY = "Authorization";
const DEFAULT_HEADERS = {
  Accept: "application/json",
  "Content-Type": "application/json; charset=utf-8"
};

const injectToken = (config) => {
  try {
    if (token.checkAccessToken()) {
      if (config.headers !== undefined) {
        config.headers[
          API_HEADER_AUTH_KEY
        ] = `Bearer ${token.getAccessToken()}`;
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
    toast.warn(msg);
  }

  return response;
};

const handleError = (error) => {
  console.log(error);
  const msg = error.response.data?.Message ?? error.response.data?.message;
  toast.error(msg);
  return error.response;
};

export class HttpInstance {
  singleton = null;

  get instance() {
    return this.singleton !== null ? this.singleton : this.initHttpInstance();
  }

  constructor() {
    this.initHttpInstance();
  }

  initHttpInstance() {
    const http = axios.create({
      baseURL: BASE_API_DOMAIN_URI,
      headers: DEFAULT_HEADERS,
      timeout: 3000
    });

    // Intercept request
    http.interceptors.request.use(injectToken, (err) => {
      Promise.reject(err);
    });

    //  Handle response
    http.interceptors.response.use(handleResponse, handleError);

    this.singleton = http;
    return http;
  }

  cancel() {
    try {
      const controller = new AbortController();
      controller.abort();
    } catch (error) {
      throw new Error(error);
    }
  }
}
