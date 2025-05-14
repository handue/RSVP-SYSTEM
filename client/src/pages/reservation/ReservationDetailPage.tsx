import { ReservationData } from "../../types/reservation";
import { ReservationDetail } from "../../components/reservation/ReservationDetail";
import { useParams } from "react-router-dom";
import { Button } from "../../components/ui/common/button";

export const ReservationDetailPage = () => {
  const { id } = useParams(); // todo: Params fetch needed
  const mockReservation: ReservationData = {
    email: "john@example.com",
    name: "John Doe",
    phone: "123-456-7890",
    store: "Hair Salon A",
    store_id: "store-1",
    store_email: "hairsalon@example.com",
    service: "Haircut",
    service_id: "service-1",
    reservation_date: "2024-03-20",
    reservation_time: "14:00",
    status: "pending" as const,
    agreedToTerms: true,
    comments: "Please be gentle with my hair",
  };

  return (
    <div>
      <div className="flex items-center mb-6">
        <Button 
          variant="link" 
          className="flex items-center text-indigo-600 hover:text-indigo-800 transition-colors duration-200 font-medium p-0"
          onClick={() => window.history.back()}
        >
          <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5 mr-1" viewBox="0 0 20 20" fill="currentColor">
            <path fillRule="evenodd" d="M9.707 16.707a1 1 0 01-1.414 0l-6-6a1 1 0 010-1.414l6-6a1 1 0 011.414 1.414L5.414 9H17a1 1 0 110 2H5.414l4.293 4.293a1 1 0 010 1.414z" clipRule="evenodd" />
          </svg>
          Back to Reservations
        </Button>
      </div>
      
      <div className="bg-white rounded-lg shadow-md p-6 md:p-8">
        <h1 className="text-2xl font-bold mb-6 text-gray-800">Reservation Details</h1>
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
    </div>
  );
};

export default ReservationDetailPage;