export interface StoreHours {
  id: string;
  storeId: string;
  day: string;
  open: string;
  close: string;
  isSpecial: boolean;
  date?: string;
}

export interface Store {
  id: string;
  name: string;
  storeEmail: string;
  location: string;
  image?: string;
}
