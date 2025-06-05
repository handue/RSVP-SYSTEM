// src/store/reservationSlice.ts
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { ReservationData, ReservationResponse } from "../types/reservation";
import { api } from "../services/api/config";
import { useNotification } from "../hooks/useNotification";
import { reservationService } from "../services/api/reservationService";
interface ReservationState {
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
  response: ReservationResponse | null;
}

const initialState: ReservationState = {
  status: "idle",
  error: null,
  response: null,
};

export const reserveSchedule = createAsyncThunk(
  "reservation/reserveSchedule",
  async (reservationData: ReservationData) => {
    const response = await reservationService.createReservation(
      reservationData
    );
    // const response = await api.post("/reservation", reservationData);
    return response;
  }
);

export const cancelReservation = createAsyncThunk(
  "reservation/cancelReservation",
  async (id: number) => {
    const response = await reservationService.cancelReservation(id);
    return response;
  }
);

const reservationSlice = createSlice({
  name: "reservation",
  initialState,
  reducers: {
    resetReservationState: (state) => {
      state.status = "idle";
      state.error = null;
      state.response = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(reserveSchedule.pending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addCase(reserveSchedule.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.response = action.payload;
      })
      .addCase(reserveSchedule.rejected, (state, action) => {
        state.status = "failed";

        state.error = action.error.message || "Failed to make reservation";
      });
  },
});

export const { resetReservationState } = reservationSlice.actions;
export default reservationSlice.reducer;
