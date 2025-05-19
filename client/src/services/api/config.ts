// src/services/api/config.ts
import axios from 'axios';

const API_URL = process.env.NODE_ENV === 'production'
  ? ''  // Empty string for relative URLs in production
  : 'http://localhost:5000';

export const api = axios.create({
  baseURL: `${API_URL}/api`,
  withCredentials: true,
  headers: {
    'Content-Type': 'application/json',
  },
});

// API 응답 인터셉터 추가
api.interceptors.response.use(
  (response) => response,
  (error) => {
    // 에러 처리 로직
    return Promise.reject(error);
  }
);