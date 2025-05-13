import { Middleware } from '@reduxjs/toolkit';
import { showNotification } from '../store/uiSlice';

interface ErrorResponse {
  message: string;
  status?: number;
  code?: string;
}

export const errorMiddleware: Middleware = (store) => (next) => (action : any) => {
  // 액션이 rejected 상태인지 확인
  if (action.type.endsWith('/rejected')) {
    const error = action.payload || action.error;
    
    // 에러 메시지 처리
    let errorMessage = 'An unexpected error occurred';
    
    if (error instanceof Error) {
      errorMessage = error.message;
    } else if (typeof error === 'string') {
      errorMessage = error;
    } else if (error && typeof error === 'object') {
      const errorResponse = error as ErrorResponse;
      errorMessage = errorResponse.message || errorMessage;
    }

    // UI 알림 표시
    store.dispatch(showNotification({
      message: errorMessage,
      type: 'error'
    }));

    // 에러 로깅 (개발 환경에서만)
    if (process.env.NODE_ENV === 'development') {
      console.error('API Error:', {
        type: action.type,
        error,
        timestamp: new Date().toISOString()
      });
    }
  }

  return next(action);
};