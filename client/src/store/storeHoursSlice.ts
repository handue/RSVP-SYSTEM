// src/store/storeHoursSlice.ts
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { StoreHour, Store } from "../types/store";
import { storeHoursService } from "../services/api/storeHoursService";

interface StoreHoursState {
  stores: Store[];
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
}

const initialState: StoreHoursState = {
  stores: [],
  status: "idle",
  error: null,
};

export const fetchStoreHours = createAsyncThunk(
  "storeHours/fetchStoreHours",
  async () => {
    const stores = await storeHoursService.getStoreHours();
    // console.log("받아온 스토어 값 확인:" + JSON.stringify(stores));
    return stores;
  }
);

export const saveStoreHours = createAsyncThunk(
  "storeHours/saveStoreHours",
  async (stores: Store[]) => {
    // console.log("저장할 스토어 값 확인:" + JSON.stringify(stores));
    const result = await storeHoursService.saveStoreHours(stores);
    return result;
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
        state.stores = action.payload;
      })
      .addCase(fetchStoreHours.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message || "Failed to fetch store hours";
      })
      .addCase(saveStoreHours.pending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addCase(saveStoreHours.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.stores = action.payload;
      })
      .addCase(saveStoreHours.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message || "Failed to save store hours";
      });

    // todo: Implementation needed, Not planned yet.
    // .addCase(updateRegularHours.fulfilled, (state, action) => {
    //   const updatedStore = action.payload;
    //   let store = state.stores.find((e) => e.id === updatedStore.id);
    //   if (store) {
    //     store = updatedStore;
    //   }
    // })
    // .addCase(updateSpecialDate.fulfilled, (state, action) => {
    //   const updatedStore = action.payload;
    //   let store = state.stores.find((e) => e.id === updatedStore.id);
    //   if (store) {
    //     store = updatedStore;
    //   }
    // })
    // .addCase(deleteSpecialDate.fulfilled, (state, action) => {
    //   const updatedStore = action.payload;
    //   let store = state.stores.find((e) => e.id === updatedStore.id);
    //   if (store) {
    //     store = updatedStore;
    //   }
    // });
  },
});

export const { resetStoreHoursState } = storeHoursSlice.actions;
export default storeHoursSlice.reducer;
