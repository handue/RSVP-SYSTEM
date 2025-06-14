// src/hooks/useStoreHours.ts
import { useCallback } from "react";
import { useAppDispatch } from "./useAppDispatch";
import { useAppSelector } from "./useAppSelector";
import {
  fetchStoreHours,
  updateRegularHours,
  updateSpecialDate,
  deleteSpecialDate,
  saveStoreHours,
} from "../store/storeHoursSlice";
import { useNotification } from "./useNotification";
import { Store } from "@/types/store";

export const useStoreHours = () => {
  const dispatch = useAppDispatch();
  const { showSuccess, showError } = useNotification();
  const { stores, status, error } = useAppSelector((state) => state.storeHours);

  // 메모이제이션: 이 함수는 dispatch가 변경될 때만 재생성됩니다.
  // 이는 불필요한 함수 재생성을 방지하고 성능을 최적화합니다.
  // Memoization: This function is recreated only when dispatch changes.
  // This prevents unnecessary function recreation and optimizes performance.
  const getStoreHours = useCallback(async () => {
    try {
      await dispatch(fetchStoreHours()).unwrap();
      showSuccess("Store hours fetched successfully");
    } catch (error) {
      console.error("Failed to fetch store hours:", error);
      showError("Failed to fetch store hours");
      throw error;
    }
  }, [dispatch]);

  const saveStoreHourHook = useCallback(async (stores: Store[]) => {
    try {
      await dispatch(saveStoreHours(stores)).unwrap();
      showSuccess("Store hours saved successfully");
    } catch (error) {
      console.error("Failed to save store hours:", error);
      showError("Failed to save store hours");
      throw error;
    }
  }, [dispatch]);

  const updateHours = useCallback(
    async (
      storeId: string,
      day: string,
      hours: { open: string; close: string }
    ) => {
      try {
        await dispatch(updateRegularHours({ storeId, day, hours })).unwrap();
      } catch (error) {
        console.error("Failed to update regular hours:", error);
        throw error;
      }
    },
    [dispatch]
  );

  const updateSpecial = useCallback(
    async (
      storeId: string,
      dateData: { date: string; hours: { open: string; close: string } }
    ) => {
      try {
        await dispatch(updateSpecialDate({ storeId, dateData })).unwrap();
      } catch (error) {
        console.error("Failed to update special date:", error);
        throw error;
      }
    },
    [dispatch]
  );

  const deleteSpecial = useCallback(
    async (storeId: string, date: string) => {
      try {
        await dispatch(deleteSpecialDate({ storeId, date })).unwrap();
      } catch (error) {
        console.error("Failed to delete special date:", error);
        throw error;
      }
    },
    [dispatch]
  );

  return {
    stores,
    status,
    error,
    getStoreHours,
    saveStoreHourHook,
    updateHours,
    updateSpecial,
    deleteSpecial,
  };
};
