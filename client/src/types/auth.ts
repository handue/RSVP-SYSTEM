// src/types/auth.ts
export interface User {
    id: string;
    email: string;
    name: string;
    role: 'admin' | 'store_manager';
    // storeId?: string;  // in case of store_manager, utilize storeID. but at this moment, don't have a plan to implement it.
  }
  
  export interface LoginCredentials {
    email: string;
    password: string;
  }