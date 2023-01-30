import storage from "./local-storage";

const ACCESS_TOKEN_KEY = "access_token";

export const getAccessToken = () => {
  return storage.get(ACCESS_TOKEN_KEY);
};

export const setAccessToken = (token) => {
  storage.set(ACCESS_TOKEN_KEY, token);
};

export const removeAccessToken = () => {
  storage.remove(ACCESS_TOKEN_KEY);
};

export const checkAccessToken = () => {
  return getAccessToken() !== undefined;
};

const token = {
  getAccessToken,
  setAccessToken,
  removeAccessToken,
  checkAccessToken
};

export default token;
