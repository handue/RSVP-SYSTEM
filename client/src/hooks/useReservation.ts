// src/hooks/useReservation.ts
import { useCallback } from "react";
import { useAppDispatch } from "./useAppDispatch";
import { useAppSelector } from "./useAppSelector";
import { reserveSchedule } from "../store/reservationSlice";
import { ReservationData } from "../types/reservation";
import { useNotification } from "./useNotification";
import { useNavigate } from "react-router-dom";
import { reservationService } from "../services/api/reservationService";
import { cancelReservation } from "../store/reservationSlice";

export const useReservation = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const { status, error, response } = useAppSelector(
    (state) => state.reservation
  );

  const { showError, showSuccess } = useNotification();

  const makeReservation = async (data: ReservationData) => {
    try {
      const response = await dispatch(reserveSchedule(data)).unwrap();
      showSuccess("Reservation created successfully");
      return response.data;
    } catch (error) {
      showError("Failed to create reservation");
      throw error;
    }
  };

  const cancelReservationHook = async (id: number) => {
    try {
      await dispatch(cancelReservation(id)).unwrap();
      showSuccess("Reservation canceled successfully");
      navigate("/");
      return;
    } catch (error) {
      console.log(error);
      showError("Failed to cancel reservation: " + error);
    }
  };

  return {
    status,
    error,
    response,
    makeReservation,
    cancelReservationHook,
  };
};
