import { AuthResponse, LoginCredentials, User } from "../../types/auth";
import { api } from "./config";

export const authService = {
  login: async (credentials: LoginCredentials): Promise<AuthResponse> => {
    const response = await api.post("/auth/login", credentials);
    return response.data;
  },

  //   todo: just remove memory token
  //   logout: async (): Promise<void> => {
  //     await api.post("/auth/logout");
  //   },
};
