import { useEffect, useState } from "react";
import { ReservationData } from "../../types/reservation";
import { ReservationDetail } from "../../components/reservation/ReservationDetail";
import { useParams } from "react-router-dom";
import { Button } from "../../components/ui/common/button";
import { reservationService } from "../../services/api/reservationService";

export const ReservationDetailPage = () => {
  const { id } = useParams();
  const [reservation, setReservation] = useState<ReservationData | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchReservation = async () => {
      try {
        if (id) {
          const response = await reservationService.getReservationById(
            parseInt(id)
          );
          console.log('데이터 확인:' , JSON.stringify(response.data))
          setReservation(response.data);
        }
      } catch (error) {
        console.error("Failed to fetch reservation:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchReservation();
  }, [id]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (!reservation) {
    return <div>Reservation not found</div>;
  }

  return (
    <div>
      <div className="flex items-center mb-6">
        <Button
          variant="link"
          className="flex items-center text-indigo-600 hover:text-indigo-800 transition-colors duration-200 font-medium p-0"
          onClick={() => window.history.back()}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            className="h-5 w-5 mr-1"
            viewBox="0 0 20 20"
            fill="currentColor"
          >
            <path
              fillRule="evenodd"
              d="M9.707 16.707a1 1 0 01-1.414 0l-6-6a1 1 0 010-1.414l6-6a1 1 0 011.414 1.414L5.414 9H17a1 1 0 110 2H5.414l4.293 4.293a1 1 0 010 1.414z"
              clipRule="evenodd"
            />
          </svg>
          Back to Reservations
        </Button>
      </div>

      <div className="bg-white border-2 border-black-100 rounded-xl p-6 md:p-8">
        <h1 className="text-2xl font-bold mb-6 text-indigo-800">
          Reservation Details
        </h1>
        <ReservationDetail
          reservation={reservation}
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
