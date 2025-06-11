// src/types/auth.ts
export interface User {
  id?: number;
  email: string;
  fullName: string;
  role: "Admin" | "Store_Manager";
  // storeId?: string;  // in case of store_manager, utilize storeID. but at this moment, don't have a plan to implement it.
}

export interface LoginCredentials {
  email: string;
  password: string;
}

export interface AuthResponse {
  success: boolean;
  data: {
    user: User;
    token: string;
  };
  error: string | null;
}
