import { Service } from "./service";
import { Store } from "./store";

export interface ReservationData {
  email: string;
  name: string;
  phone: string;
  store: Store['name'];
  store_id: Store['id'];
  service: Service['name'];
  service_id: Service['id'];
  store_email: Store["storeEmail"];
  reservation_date: string;
  reservation_time: string;
  comments?: string;
  agreedToTerms: boolean;
  // isAdvertisement?: boolean;
  status: "pending" | "confirmed" | "cancelled";
}

export interface ReservationResponse {
  status: number;
  data?: {
    message: string;
  };
}
