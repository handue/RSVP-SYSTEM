// hooks/useNotification.ts
import { useAppDispatch } from "./useAppDispatch";
import { addNotification, NotificationType } from "../store/notificationSlice";

export function useNotification() {
  const dispatch = useAppDispatch();

  const showNotification = (
    message: string,
    type: NotificationType = "info",
    duration: number = 5000
  ) => {
    dispatch(
      addNotification({
        message,
        type,
        duration,
      })
    );
  };

  return {
    showSuccess: (message: string, duration?: number) =>
      showNotification(message, "success", duration),
    showError: (message: string, duration?: number) =>
      showNotification(message, "error", duration),
    showInfo: (message: string, duration?: number) =>
      showNotification(message, "info", duration),
    showWarning: (message: string, duration?: number) =>
      showNotification(message, "warning", duration),
    showLoading: (message: string, duration?: number) =>
      showNotification(message, "loading", duration),
  };
}