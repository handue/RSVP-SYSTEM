// src/services/api/reservationService.ts
import { api } from "./config";
import { ReservationData } from "../../types/reservation";

export const reservationService = {
  createReservation: async (reservationData: ReservationData) => {
    const response = await api.post("/reservation", reservationData);
    return response.data;
  },

  cancelReservation: async (id: number) => {
    const response = await api.delete(`/reservation/${id}`);
    return response.data;
  },

  getReservationById: async (id: number) => {
    const response = await api.get(`/reservation/${id}`);
    return response.data;
  },
};
