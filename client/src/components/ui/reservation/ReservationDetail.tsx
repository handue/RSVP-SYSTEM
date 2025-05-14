import { Button } from "../common/button";
import { ReservationData } from "../../../types/reservation";

// interface Reservation {
//   id: string;
//   name: string;
//   email: string;
//   phone: string;
//   service: string;
//   reservation_date: string;
//   reservation_time: string;
//   status: "pending" | "confirmed" | "cancelled";
//   comments?: string;
// }

interface ReservationDetailProps {
  reservation: ReservationData;
  onEdit?: () => void;
  onCancel?: () => void;
}

export function ReservationDetail({ reservation, onEdit, onCancel }: ReservationDetailProps) {
  const getStatusColor = (status: ReservationData["status"]) => {
    switch (status) {
      case "pending":
        return "bg-yellow-100 text-yellow-800";
      case "confirmed":
        return "bg-green-100 text-green-800";
      case "cancelled":
        return "bg-red-100 text-red-800";
      default:
        return "bg-gray-100 text-gray-800";
    }
  };

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-2xl font-bold">Reservation Details</h2>
        <div className="flex space-x-2">
          <Button variant="outline" onClick={onEdit}>
            Edit
          </Button>
          <Button variant="destructive" onClick={onCancel}>
            Cancel
          </Button>
        </div>
      </div>

      <div className="grid grid-cols-2 gap-4">
        <div className="space-y-2">
          <h3 className="text-sm font-medium text-gray-500">Name</h3>
          <p>{reservation.name}</p>
        </div>

        <div className="space-y-2">
          <h3 className="text-sm font-medium text-gray-500">Email</h3>
          <p>{reservation.email}</p>
        </div>

        <div className="space-y-2">
          <h3 className="text-sm font-medium text-gray-500">Phone</h3>
          <p>{reservation.phone}</p>
        </div>

        <div className="space-y-2">
          <h3 className="text-sm font-medium text-gray-500">Service</h3>
          <p>{reservation.service}</p>
        </div>

        <div className="space-y-2">
          <h3 className="text-sm font-medium text-gray-500">Date</h3>
          <p>{reservation.reservation_date}</p>
        </div>

        <div className="space-y-2">
          <h3 className="text-sm font-medium text-gray-500">Time</h3>
          <p>{reservation.reservation_time}</p>
        </div>

        <div className="space-y-2">
          <h3 className="text-sm font-medium text-gray-500">Status</h3>
          <span className={`px-2 py-1 rounded-full text-xs ${getStatusColor(reservation.status)}`}>
            {reservation.status.charAt(0).toUpperCase() + reservation.status.slice(1)}
          </span>
        </div>

        {reservation.comments && (
          <div className="col-span-2 space-y-2">
            <h3 className="text-sm font-medium text-gray-500">Additional Requests</h3>
            <p>{reservation.comments}</p>
          </div>
        )}
      </div>
    </div>
  );
} 