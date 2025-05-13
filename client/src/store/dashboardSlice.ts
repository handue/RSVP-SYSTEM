import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { dashboardService, DashboardStats, TodayReservation } from "../services/api/dashboardService";

interface DashboardState {
  stats: DashboardStats | null;
  todayReservations: TodayReservation[];
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: DashboardState = {
  stats: null,
  todayReservations: [],
  status: "idle",
  error: null,
};

export const fetchDashboardData = createAsyncThunk(
  "dashboard/fetchData",
  async () => {
    const [stats, todayReservations] = await Promise.all([
      dashboardService.getDashboardStats(),
      dashboardService.getTodayReservations(),
    ]);
    return { stats, todayReservations };
  }
);

export const confirmReservation = createAsyncThunk(
  "dashboard/confirmReservation",
  async (reservationId: string) => {
    await dashboardService.confirmReservation(reservationId);
    return reservationId;
  }
);

export const cancelReservation = createAsyncThunk(
  "dashboard/cancelReservation",
  async (reservationId: string) => {
    await dashboardService.cancelReservation(reservationId);
    return reservationId;
  }
);

const dashboardSlice = createSlice({
  name: "dashboard",
  initialState,
  reducers: {
    resetDashboardState: (state) => {
      state.stats = null;
      state.todayReservations = [];
      state.status = "idle";
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchDashboardData.pending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addCase(fetchDashboardData.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.stats = action.payload.stats;
        state.todayReservations = action.payload.todayReservations;
      })
      .addCase(fetchDashboardData.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message || "Failed to fetch dashboard data";
      })
      .addCase(confirmReservation.fulfilled, (state, action) => {
        const reservation = state.todayReservations.find(
          (r) => r.id === action.payload
        );
        if (reservation) {
          reservation.status = "confirmed";
        }
      })
      .addCase(cancelReservation.fulfilled, (state, action) => {
        const reservation = state.todayReservations.find(
          (r) => r.id === action.payload
        );
        if (reservation) {
          reservation.status = "cancelled";
        }
      });
  },
});

export const { resetDashboardState } = dashboardSlice.actions;
export default dashboardSlice.reducer; 