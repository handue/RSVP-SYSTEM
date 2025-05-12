// src/services/api/reservationService.ts
import { api } from "./config";
import { ReservationData } from "../../types/reservation";

export const reservationService = {
  createReservation: async (reservationData: ReservationData) => {
    const response = await api.post("/reservation/calendar", {
      reservationData,
    });
    return response.data;
  },

  sendEmail: async (emailData: {
    email: string;
    name: string;
    reservationDetails: any;
  }) => {
    const response = await api.post("/reservation/send", emailData);
    return response.data;
  },
};
