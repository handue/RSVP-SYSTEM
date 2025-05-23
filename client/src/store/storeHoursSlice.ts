// src/store/storeHoursSlice.ts
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { StoreHours } from "../types/store";
import { storeHoursService } from "../services/api/storeHoursService";

interface StoreHoursState {
  hours: StoreHours[];
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: StoreHoursState = {
  hours: [],
  status: "idle",
  error: null,
};

export const fetchStoreHours = createAsyncThunk(
  "storeHours/fetchStoreHours",
  async () => {
    const response = await storeHoursService.getStoreHours();
    return response.storeHours || response.data;
  }
);

export const updateRegularHours = createAsyncThunk(
  "storeHours/updateRegularHours",
  async ({
    storeId,
    day,
    hours,
  }: {
    storeId: string;
    day: string;
    hours: { open: string; close: string };
  }) => {
    const response = await storeHoursService.updateRegularHours(
      storeId,
      day,
      hours
    );
    return response.storeHours || response.data;
  }
);

export const updateSpecialDate = createAsyncThunk(
  "storeHours/updateSpecialDate",
  async ({
    storeId,
    dateData,
  }: {
    storeId: string;
    dateData: { date: string; hours: { open: string; close: string } };
  }) => {
    const response = await storeHoursService.updateSpecialDate(
      storeId,
      dateData
    );
    return response.storeHours || response.data;
  }
);

export const deleteSpecialDate = createAsyncThunk(
  "storeHours/deleteSpecialDate",
  async ({ storeId, date }: { storeId: string; date: string }) => {
    const response = await storeHoursService.deleteSpecialDate(storeId, date);
    return response.storeHours || response.data;
  }
);

const storeHoursSlice = createSlice({
  name: "storeHours",
  initialState,
  reducers: {
    resetStoreHoursState: (state) => {
      state.status = "idle";
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchStoreHours.pending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addCase(fetchStoreHours.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.hours = action.payload;
      })
      .addCase(fetchStoreHours.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message || "Failed to fetch store hours";
      })
      .addCase(updateRegularHours.fulfilled, (state, action) => {
        state.hours = action.payload;
      })
      .addCase(updateSpecialDate.fulfilled, (state, action) => {
        state.hours = action.payload;
      })
      .addCase(deleteSpecialDate.fulfilled, (state, action) => {
        state.hours = action.payload;
      });
  },
});

export const { resetStoreHoursState } = storeHoursSlice.actions;
export default storeHoursSlice.reducer;
