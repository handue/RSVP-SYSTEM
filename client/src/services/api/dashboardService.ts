import { api } from "./config";

export interface DashboardStats {
  totalReservations: number;
  todayReservations: number;
  pendingReservations: number;
  confirmedReservations: number;
}

export interface TodayReservation {
  id: string;
  name: string;
  service: string;
  time: string;
  status: "pending" | "confirmed" | "cancelled";
}

export const dashboardService = {
  getDashboardStats: async (): Promise<DashboardStats> => {
    const response = await api.get("/dashboard/stats");
    return response.data;
  },

  getTodayReservations: async (): Promise<TodayReservation[]> => {
    const response = await api.get("/dashboard/today-reservations");
    return response.data;
  },

  confirmReservation: async (reservationId: string): Promise<void> => {
    await api.put(`/dashboard/reservations/${reservationId}/confirm`);
  },

  cancelReservation: async (reservationId: string): Promise<void> => {
    await api.put(`/dashboard/reservations/${reservationId}/cancel`);
  },
}; 