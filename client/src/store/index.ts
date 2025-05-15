// src/store/index.ts
import { configureStore } from "@reduxjs/toolkit";
import storeHoursReducer from "./storeHoursSlice";
import reservationReducer from "./reservationSlice";
import uiReducer from "./uiSlice"; // 추가 필요
import { errorMiddleware } from "../middleware/errorMiddleware";
import notificationReducer from "./notificationSlice";
import dashboardReducer from "./dashboardSlice";
import authReducer from "./authSlice";

export const store = configureStore({
  reducer: {
    storeHours: storeHoursReducer,
    reservation: reservationReducer,
    ui: uiReducer,
    notification: notificationReducer,
    dashboard: dashboardReducer,
    auth: authReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: false, // Date 객체 처리를 위해
    }).concat(errorMiddleware), // concat = 기본 배열에 errorMiddleware 추가해서 새로운 배열로 반환
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
