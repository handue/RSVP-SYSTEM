// store/slices/notificationSlice.ts
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export type NotificationType =
  | "success"
  | "error"
  | "info"
  | "warning"
  | "loading";

export interface Notification {
  id: string;
  type: NotificationType;
  message: string;
  duration?: number;
}

interface NotificationState {
  notifications: Notification[];
}

const initialState: NotificationState = {
  notifications: [],
};

export const notificationSlice = createSlice({
  name: "notification",
  initialState,
  reducers: {
    addNotification: (
      state,
      action: PayloadAction<Omit<Notification, "id">>
    ) => {
      const id = Date.now().toString();
      state.notifications.push({
        id,
        ...action.payload,
      });
    },
    removeNotification: (state, action: PayloadAction<string>) => {
      state.notifications = state.notifications.filter(
        (notification) => notification.id !== action.payload
      );
    },
  },
});

export const { addNotification, removeNotification } =
  notificationSlice.actions;

export const selectNotifications = (state: {
  notification: NotificationState;
}) => state.notification.notifications;

export default notificationSlice.reducer;
