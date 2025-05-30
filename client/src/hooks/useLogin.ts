import { useAppDispatch } from "./useAppDispatch";
import { useNotification } from "./useNotification";
import { useCallback } from "react";
import { login } from "../store/authSlice";
export const useLogin = () => {
  const dispatch = useAppDispatch();
  const { showError, showSuccess } = useNotification();

  const loginHook = useCallback(
    async (email: string, password: string) => {
      try {
        await dispatch(login({ email, password })).unwrap();
        showSuccess("Login successful");
        return true;
      } catch (error) {
        showError("Login failed");
        throw error;
      }
    },
    [dispatch]
  );

  return { loginHook };
};
