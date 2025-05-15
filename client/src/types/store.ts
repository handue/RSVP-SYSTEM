export interface StoreHours {
  storeId: Store["id"];
  regularHours: {
    day: string;
    open: string;
    close: string;
    isClosed: boolean;
  }[];
  specialDate?: {
    date: specialDate["date"];
    open: specialDate["open"];
    close: specialDate["close"];
    isClosed: specialDate["isClosed"];
  }[];
}

export interface Store {
  id: string;
  name: string;
  storeEmail: string;
  location: string;
  image?: string;
}

export interface specialDate {
  date: string;
  open: string;
  close: string;
  isClosed: boolean;
}
