import { HttpInstance } from "../ApiWrapper/http-instances";
import token from "../token";

const http = new HttpInstance();

export const authProvider = {
  login: async ({ username, password }) => {
    const result = await http.instance.post(`Accounts/sign-in`, {
      email: username,
      password
    });

    token.setAccessToken(result.data.data.accessToken);
  },
  logout: () => {
    token.removeAccessToken();
    return Promise.resolve();
  },
  checkAuth: () => {
    const accessToken = token.getAccessToken();
    return accessToken ? Promise.resolve() : Promise.reject();
  },
  getPermissions: () => {
    // Required for the authentication to work
    return Promise.resolve();
  }
};
