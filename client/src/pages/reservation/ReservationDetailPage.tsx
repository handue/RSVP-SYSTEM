import { ReservationData } from "../../types/reservation";
import { ReservationDetail } from "../../components/ui/reservation/ReservationDetail";
import { useParams } from "react-router-dom";

export const ReservationDetailPage = () => {
  const { id } = useParams();
// todo: Params 로 fetch 해서 데이터 받아와야함. 
  const mockReservation: ReservationData = {
    email: "john@example.com",
    name: "John Doe",
    phone: "123-456-7890",
    store: "Hair Salon A",
    store_id: "store-1",
    store_email: "hairsalon@example.com",
    service: "Haircut",
    reservation_date: "2024-03-20",
    reservation_time: "14:00",
    status: "pending" as const,
    agreedToTerms: true,
    comments: "Please be gentle with my hair",
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Reservation Details</h1>
      <ReservationDetail
        reservation={mockReservation}
        onEdit={() => {
          console.log("edit");
        }}
        onCancel={() => {
          console.log("cancel");
        }}
      />
    </div>
  );
};

export default ReservationDetailPage;
