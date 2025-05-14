import { useEffect } from "react";
import { useReservation } from "../../../hooks/useReservation";
import { Button } from "../common/button";

// todo: reservation List의 존재 의의가 뭐지 지금, 이런거 보여주는게 아니라 예약 몇시몇시 있는지 체크하는거 아닌가, 그러면 이름,스토어,시간, 서비스 이것만 보여줘야 할거 같은데.
interface Reservation {
  id: string;
  name: string;
  email: string;
  phone: string;
  service: string;
  reservation_date: string;
  reservation_time: string;
  status: "pending" | "confirmed" | "cancelled";
}

interface ReservationListProps {
  storeId: string;
  onEdit?: (reservation: Reservation) => void;
}

export function ReservationList({ storeId, onEdit }: ReservationListProps) {
  const { status, error, response } = useReservation();

  useEffect(() => {
    // TODO: Fetch reservations for the store
  }, [storeId]);

  if (status === "loading") {
    return <div>Loading reservations...</div>;
  }

  if (error) {
    return <div>Error loading reservations: {error}</div>;
  }

  return (
    <div className="space-y-4">
      <div className="flex justify-between items-center">
        <h2 className="text-2xl font-bold">Reservations</h2>
        <Button variant="outline">Filter</Button>
      </div>

      <div className="overflow-x-auto">
        <table className="w-full">
          <thead>
            <tr className="border-b">
              <th className="text-left p-2">Name</th>
              <th className="text-left p-2">Service</th>
              <th className="text-left p-2">Date</th>
              <th className="text-left p-2">Time</th>
              <th className="text-left p-2">Status</th>
              <th className="text-left p-2">Actions</th>
            </tr>
          </thead>
          <tbody>
            {/* TODO: Map through reservations */}
            <tr className="border-b">
              <td className="p-2">John Doe</td>
              <td className="p-2">Haircut</td>
              <td className="p-2">2024-03-20</td>
              <td className="p-2">14:00</td>
              <td className="p-2">
                <span className="px-2 py-1 rounded-full text-xs bg-yellow-100 text-yellow-800">
                  Pending
                </span>
              </td>
              <td className="p-2">
                <div className="flex space-x-2">
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() =>
                      onEdit?.({
                        id: "1",
                        name: "John Doe",
                        email: "john@example.com",
                        phone: "123-456-7890",
                        service: "haircut",
                        reservation_date: "2024-03-20",
                        reservation_time: "14:00",
                        status: "pending",
                      })
                    }
                  >
                    Edit
                  </Button>
                  <Button variant="destructive" size="sm">
                    Cancel
                  </Button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
}
