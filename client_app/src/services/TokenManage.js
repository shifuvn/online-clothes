export const ACCESS_TOKEN_KEY = "access_token";

const getAccessToken = () => {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
};

const setAccessToken = (tokenValue) => {
  localStorage.setItem(ACCESS_TOKEN_KEY, tokenValue);
};

const removeAccessToken = () => {
  localStorage.removeItem(ACCESS_TOKEN_KEY);
};

const checkAccessToken = () => {
  return getAccessToken() !== undefined;
};

const isAuth = () => {
  return getAccessToken() !== undefined && getAccessToken() !== null;
};

export const TokenManage = {
  getAccessToken,
  setAccessToken,
  removeAccessToken,
  checkAccessToken,
  isAuth
};
