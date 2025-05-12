// src/store/uiSlice.ts
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface UIState {
  notification: {
    show: boolean;
    message: string;
    type: 'success' | 'error' | 'info';
  };
  loading: {
    [key: string]: boolean;
  };
}

const initialState: UIState = {
  notification: {
    show: false,
    message: '',
    type: 'info',
  },
  loading: {},
};

const uiSlice = createSlice({
  name: 'ui',
  initialState,
  reducers: {
    showNotification: (state, action: PayloadAction<{ message: string; type: 'success' | 'error' | 'info' }>) => {
      state.notification = {
        show: true,
        message: action.payload.message,
        type: action.payload.type,
      };
    },
    hideNotification: (state) => {
      state.notification.show = false;
    },
    setLoading: (state, action: PayloadAction<{ key: string; value: boolean }>) => {
      state.loading[action.payload.key] = action.payload.value;
    },
  },
});

export const { showNotification, hideNotification, setLoading } = uiSlice.actions;
export default uiSlice.reducer;