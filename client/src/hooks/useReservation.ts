// src/hooks/useReservation.ts
import { useCallback } from "react";
import { useAppDispatch } from "./useAppDispatch";
import { useAppSelector } from "./useAppSelector";
import { reserveSchedule, sendEmail } from "../store/reservationSlice";
import { ReservationData } from "../types/reservation";
import { useNotification } from "./useNotification";
import { useNavigate } from "react-router-dom";
import { reservationService } from "../services/api/reservationService";

export const useReservation = () => {
  const dispatch = useAppDispatch();
  const { status, error, response } = useAppSelector(
    (state) => state.reservation
  );

  const { showError, showSuccess } = useNotification();

  const makeReservation = async (data: ReservationData) => {
    try {
      const response = await dispatch(reserveSchedule(data)).unwrap();
      showSuccess("Reservation created successfully");
      return response.data.data;
    } catch (error) {
      showError("Failed to create reservation");
      throw error;
    }
  };


  const sendReservationEmail = useCallback(
    async (emailData: {
      email: string;
      name: string;
      reservationDetails: any;
    }) => {
      try {
        await dispatch(sendEmail(emailData)).unwrap();
      } catch (error) {
        console.error("Failed to send email:", error);
        throw error;
      }
    },
    [dispatch]
  );

  return {
    status,
    error,
    response,
    makeReservation,
    sendReservationEmail,
  };
};
