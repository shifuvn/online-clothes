import axios from "axios";
import token from "../token";

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

export class HttpInstance {
  singleton = null;

  get instance() {
    return this.singleton !== null ? this.singleton : this.initHttpInstance();
  }

  initHttpInstance() {
    const http = axios.create({
      baseURL: BASE_API_DOMAIN_URI,
      headers: DEFAULT_HEADERS,
      timeout: 3000
    });

    http.interceptors.request.use(injectToken, (err) => {
      Promise.reject(err);
    });

    http.interceptors.response.use(
      (res) => res,
      (err) => {
        return Promise.reject(err);
      }
    );

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
