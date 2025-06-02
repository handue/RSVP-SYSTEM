// src/services/api/storeHoursService.ts
import { api } from './config';
import { StoreHour } from '../../types/store';

export const storeHoursService = {
  getStoreHours: async () => {
    const response = await api.get('/store');
    return response.data.data;
  },

  updateRegularHours: async (storeId: string, day: string, hours: { open: string; close: string }) => {
    const response = await api.put(`/store-hours/regular-hours/${storeId}`, { day, hours });
    return response.data.data;
  },

  updateSpecialDate: async (storeId: string, dateData: { date: string; hours: { open: string; close: string } }) => {
    const response = await api.put(`/store-hours/special-date/${storeId}`, dateData);
    return response.data.data;
  },

  deleteSpecialDate: async (storeId: string, date: string) => {
    const response = await api.delete(`/store-hours/special-date/${storeId}/${date}`);
    return response.data.data;
  },
};

