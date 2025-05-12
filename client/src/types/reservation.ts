export interface ReservationData {
  email: string;
  name: string;
  phone: string;
  store: string;
  store_id: string;
  service: string;
  store_email: string;
  reservation_date: string;
  reservation_time: string;
  comments?: string;
  agreedToTerms: boolean;
  isAdvertisement: boolean;
}

export interface ReservationResponse {
  status: number;
  data?: {
    message: string;
  };
}
