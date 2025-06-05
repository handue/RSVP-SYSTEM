import { Button } from "../ui/common/button";
import { ReservationData } from "../../types/reservation";
import { stores } from "./StoreSelection";

interface ReservationDetailProps {
  reservation: ReservationData;
  onEdit?: () => void;
  onCancel?: () => void;
}

export function ReservationDetail({
  reservation,
  onEdit,
  onCancel,
}: ReservationDetailProps) {
  // const getStatusColor = (status: ReservationData["status"]) => {
  //   switch (status) {
  //     case "pending":
  //       return "bg-yellow-100 text-yellow-800 border-yellow-200";
  //     case "confirmed":
  //       return "bg-emerald-100 text-emerald-800 border-emerald-200";
  //     case "cancelled":
  //       return "bg-gray-100 text-gray-800 border-gray-200";
  //     default:
  //       return "bg-gray-100 text-gray-800 border-gray-200";
  //   }
  // };

  const formatDate = (dateString: string) => {
    const options: Intl.DateTimeFormatOptions = {
      year: "numeric",
      month: "long",
      day: "numeric",
    };
    return new Date(dateString).toLocaleDateString(undefined, options);
  };

  const formatTime = (timeString: string) => {
    const [hours, minutes] = timeString.split(":");
    const date = new Date();
    date.setHours(parseInt(hours), parseInt(minutes));
    return date.toLocaleTimeString(undefined, {
      hour: "2-digit",
      minute: "2-digit",
    });
  };

  return (
    <div className="space-y-8">
      {/* Summary Section */}
      <div className=" border border-indigo-400 rounded-lg p-5">
        <div className="flex flex-col md:flex-row justify-between">
          <div className="mb-4 md:mb-0">
            <h3 className="text-sm font-medium text-indigo-700 mb-1">
              Appointment
            </h3>
            <p className="text-lg font-bold text-gray-900">
              {reservation.service}
            </p>
            <p className="text-gray-700 mt-1">
              {formatDate(reservation.reservation_date)} at{" "}
              {formatTime(reservation.reservation_time)}
            </p>
          </div>

          <div className="flex flex-col justify-between gap-y-4">
            {/* <div className="text-center">
              <h3 className="text-sm font-medium text-indigo-700 mb-1">
                Status:{" "}
                <span
                  className={`inline-flex items-center px-2 py-1 ml-3 rounded-full text-sm font-medium border ${getStatusColor(
                    reservation.status
                  )}`}
                >
                  {reservation.status.charAt(0).toUpperCase() +
                    reservation.status.slice(1)}
                </span>
              </h3>
            </div> */}

            <div className="flex justify-end mt-4 md:mt-0 space-x-3">
              <Button
                variant="outline"
                onClick={onEdit}
                className="border-indigo-200 border-2 text-indigo-700 hover:bg-indigo-100"
              >
                Edit
              </Button>
              {reservation.status !== "cancelled" && (
                <Button
                  variant="destructive"
                  onClick={(e) => {
                    e.stopPropagation();
                    onCancel?.();
                  }}
                  className="bg-white border-red-300 text-red-500 border-2 hover:bg-red-50"
                >
                  Cancel
                </Button>
              )}
            </div>
          </div>
        </div>
      </div>

      {/* Details Section */}
      <div>
        <h3 className="text-lg font-semibold text-gray-800 mb-4">
          Customer Information
        </h3>
        <div className="bg-white border border- shadow-sm rounded-lg overflow-hidden">
          <div className="grid grid-cols-1 md:grid-cols-2 divide-y md:divide-y-0 md:divide-x divide-gray-300 border border-gray-300">
            <div className="p-4">
              <h4 className="text-sm font-medium text-gray-500 mb-1">
                Full Name
              </h4>
              <p className="text-gray-800 font-semibold">{reservation.name}</p>
            </div>

            <div className="p-4">
              <h4 className="text-sm font-medium text-gray-500 mb-1">
                Phone Number
              </h4>
              <p className="text-gray-800 font-semibold">{reservation.phone}</p>
            </div>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 divide-y md:divide-y-0 md:divide-x divide-gray-300 border border-gray-300">
            <div className="p-4">
              <h4 className="text-sm font-medium text-gray-500 mb-1">
                Email Address
              </h4>
              <p className="text-gray-800 font-semibold">{reservation.email}</p>
            </div>

            <div className="p-4">
              <h4 className="text-sm font-medium text-gray-500 mb-1">
                Agreed to Terms
              </h4>
              <p className="text-gray-800 font-semibold">
                {reservation.agreedToTerms ? "Yes" : "No"}
              </p>
            </div>
          </div>
        </div>
      </div>

      {/* Location Section */}
      <div>
        <h3 className="text-lg font-semibold text-gray-800 mb-4">Location</h3>
        <div className="bg-white border border-gray-300 shadow-sm rounded-lg p-4">
          <div className="flex items-start">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-5 w-5 text-gray-500 mt-1 mr-3 flex-shrink-0"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z"
              />
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M15 11a3 3 0 11-6 0 3 3 0 016 0z"
              />
            </svg>
            <div>
              <p className="font-medium text-gray-900">
                {stores.find((store) => store.storeId === reservation.store_id)
                  ?.name +
                  " - " +
                  stores.find((store) => store.storeId === reservation.store_id)
                    ?.location || "No Store Selected"}
              </p>
              <p className="text-gray-600 text-sm mt-1">
                Contact:{" "}
                {stores.find((store) => store.storeId === reservation.store_id)
                  ?.storeEmail || "No Store Email"}
              </p>
            </div>
          </div>
        </div>
      </div>

      {/* Additional Requests Section */}
      {reservation.comments && (
        <div>
          <h3 className="text-lg font-semibold text-gray-800 mb-4">
            Additional Requests
          </h3>
          <div className="bg-white border border-gray-300 shadow-sm rounded-lg p-4">
            <p className="text-gray-700">{reservation.comments}</p>
          </div>
        </div>
      )}
    </div>
  );
}
