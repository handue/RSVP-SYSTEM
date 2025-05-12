// src/store/index.ts
import { configureStore } from "@reduxjs/toolkit";
import storeHoursReducer from "./storeHoursSlice";
import reservationReducer from "./reservationSlice";
import uiReducer from "./uiSlice"; // 추가 필요

export const store = configureStore({
  reducer: {
    storeHours: storeHoursReducer,
    reservation: reservationReducer,
    ui: uiReducer, // 추가 필요
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: false, // Date 객체 처리를 위해
    }),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
