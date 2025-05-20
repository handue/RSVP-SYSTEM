// src/hooks/useReservation.ts
import { useCallback } from "react";
import { useAppDispatch } from "./useAppDispatch";
import { useAppSelector } from "./useAppSelector";
import { reserveSchedule, sendEmail } from "../store/reservationSlice";
import { ReservationData } from "../types/reservation";

export const useReservation = () => {
  const dispatch = useAppDispatch();
  const { status, error, response } = useAppSelector(
    (state) => state.reservation
  );

  const makeReservation = useCallback(
    async (reservationData: ReservationData) => {
      try {
        const result = await dispatch(
          reserveSchedule(reservationData)
        ).unwrap();
        return result;
      } catch (error) {
        console.error("Failed to make reservation:", error);
        throw error;
      }
    },
    [dispatch]
  );

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
