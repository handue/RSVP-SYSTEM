// src/store/authSlice.ts
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { User, LoginCredentials } from "../types/auth";
import { authService } from "../services/api/authService";

interface AuthState {
  user: User | null;
  status: "idle" | "loading" | "succeeded" | "failed";
  error: string | null;
  token: string | null;
}

const initialState: AuthState = {
  user: null,
  status: "idle",
  error: null,
  token: null,
};

export const login = createAsyncThunk(
  "auth/login",
  async (credentials: LoginCredentials, { rejectWithValue }) => {
    const response = await authService.login(credentials);
    console.log("return value check:" + JSON.stringify(response, null, 2));

    return response;

    // // TODO: API 연동
    // // 임시로 mock 데이터 반환
    // return {
    //   id: 1,
    //   email: credentials.email,
    //   name: "Admin User",
    //   role: "admin",
    // } as User;
  }
);

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    logout: (state) => {
      state.user = null;
      state.status = "idle";
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.pending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addCase(login.fulfilled, (state, action) => {
        state.status = "succeeded";
        // console.log("action.payload value check:" + JSON.stringify(action.payload, null, 2));
        state.user = action.payload.data.user;
        state.token = action.payload.data.token;
      })
      .addCase(login.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message || "Login failed";
      });
  },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;
