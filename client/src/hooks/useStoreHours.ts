// src/hooks/useStoreHours.ts
import { useCallback } from 'react';
import { useAppDispatch } from './useAppDispatch';
import { useAppSelector } from './useAppSelector';
import { 
  fetchStoreHours, 
  updateRegularHours, 
  updateSpecialDate, 
  deleteSpecialDate 
} from '../store/storeHoursSlice';

export const useStoreHours = () => {
  const dispatch = useAppDispatch();
  const { hours, status, error } = useAppSelector((state) => state.storeHours);

  const getStoreHours = useCallback(async () => {
    try {
      await dispatch(fetchStoreHours()).unwrap();
    } catch (error) {
      console.error('Failed to fetch store hours:', error);
      throw error;
    }
  }, [dispatch]);

  const updateHours = useCallback(async (storeId: string, day: string, hours: { open: string; close: string }) => {
    try {
      await dispatch(updateRegularHours({ storeId, day, hours })).unwrap();
    } catch (error) {
      console.error('Failed to update regular hours:', error);
      throw error;
    }
  }, [dispatch]);

  const updateSpecial = useCallback(async (storeId: string, dateData: { date: string; hours: { open: string; close: string } }) => {
    try {
      await dispatch(updateSpecialDate({ storeId, dateData })).unwrap();
    } catch (error) {
      console.error('Failed to update special date:', error);
      throw error;
    }
  }, [dispatch]);

  const deleteSpecial = useCallback(async (storeId: string, date: string) => {
    try {
      await dispatch(deleteSpecialDate({ storeId, date })).unwrap();
    } catch (error) {
      console.error('Failed to delete special date:', error);
      throw error;
    }
  }, [dispatch]);

  return {
    hours,
    status,
    error,
    getStoreHours,
    updateHours,
    updateSpecial,
    deleteSpecial,
  };
};

