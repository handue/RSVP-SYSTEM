export interface StoreHour {
  id?: number;
  storeId: Store["storeId"];
  regularHours: RegularHours[];
  specialDate?: SpecialDate[];
}

// * same with StoreResponseDto
export interface Store {
  id?: number;
  storeId: string;
  name: string;
  location: string;
  storeEmail: string;
  storeHour: StoreHour;
  // image?: string;
}

export interface RegularHours {
  id?: number;
  storeHourId?: number;
  day: number;
  open: string;
  close: string;
  isClosed: boolean;
}

export interface SpecialDate {
  id?: number;
  date: string;
  open: string;
  close: string;
  isClosed: boolean;
}
