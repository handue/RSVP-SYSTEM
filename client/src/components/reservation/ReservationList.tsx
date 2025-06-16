import { useEffect, useState } from "react";
import { useReservation } from "../../hooks/useReservation";
import { Button } from "../ui/common/button";

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
  storeName?: string;
  onEdit?: (reservation: Reservation) => void;
}

// ! Not Used in this project. made it just in case.
export function ReservationList({
  storeId,
  storeName,
  onEdit,
}: ReservationListProps) {
  const { status, error, response } = useReservation();
  const [searchTerm, setSearchTerm] = useState("");

  useEffect(() => {
    // TODO: Fetch reservations for the store
  }, [storeId]);

  if (status === "loading") {
    return (
      <div className="flex justify-center items-center py-12">
        <div className="flex flex-col items-center">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-indigo-600 mb-3"></div>
          <p className="text-gray-600">Loading reservations...</p>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-md">
        <p className="flex items-center">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            className="h-5 w-5 mr-2"
            viewBox="0 0 20 20"
            fill="currentColor"
          >
            <path
              fillRule="evenodd"
              d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z"
              clipRule="evenodd"
            />
          </svg>
          Error loading reservations: {error}
        </p>
      </div>
    );
  }

  return (
    <div className="space-y-5">
      <div className="flex flex-col md:flex-row md:justify-between md:items-center gap-4">
        <div>
          <h2 className="text-xl font-bold text-gray-800">
            {storeName ? `${storeName} Reservations` : "Reservations"}
          </h2>
          <p className="text-sm text-gray-500 mt-1">
            Manage and view all reservations
          </p>
        </div>

        <div className="flex items-center space-x-3">
          <div className="relative">
            <input
              type="text"
              placeholder="Search reservations..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="pl-9 pr-4 py-2 border border-gray-300 rounded-md focus:ring-indigo-500 focus:border-indigo-500 w-full md:w-auto"
            />
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-5 w-5 text-gray-400 absolute left-2 top-1/2 transform -translate-y-1/2"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"
              />
            </svg>
          </div>

          <Button
            variant="outline"
            className="border-indigo-200 text-indigo-700 hover:bg-indigo-50 flex items-center"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-4 w-4 mr-1"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M3 4a1 1 0 011-1h16a1 1 0 011 1v2.586a1 1 0 01-.293.707l-6.414 6.414a1 1 0 00-.293.707V17l-4 4v-6.586a1 1 0 00-.293-.707L3.293 7.293A1 1 0 013 6.586V4z"
              />
            </svg>
            Filter
          </Button>
        </div>
      </div>

      <div className="overflow-x-auto bg-white rounded-lg shadow-sm border border-gray-200">
        <table className="w-full">
          <thead>
            <tr className="bg-gray-50 border-b border-gray-200">
              <th className="text-left p-4 font-medium text-gray-600">Name</th>
              <th className="text-left p-4 font-medium text-gray-600">
                Service
              </th>
              <th className="text-left p-4 font-medium text-gray-600">Date</th>
              <th className="text-left p-4 font-medium text-gray-600">Time</th>
              <th className="text-left p-4 font-medium text-gray-600">
                Status
              </th>
              <th className="text-left p-4 font-medium text-gray-600">
                Actions
              </th>
            </tr>
          </thead>
          <tbody>
            {/* TODO: Map through reservations */}
            {/* TODO: Search function implement needed */}
            <tr className="border-b border-gray-200 hover:bg-gray-50 transition-colors duration-150">
              <td className="p-4 font-medium text-gray-700">John Doe</td>
              <td className="p-4 text-gray-700">Haircut</td>
              <td className="p-4 text-gray-700">2024-03-20</td>
              <td className="p-4 text-gray-700">14:00</td>
              <td className="p-4">
                <span className="px-3 py-1 rounded-full text-xs font-medium bg-yellow-100 text-yellow-800">
                  Pending
                </span>
              </td>
              <td className="p-4">
                <div className="flex space-x-2">
                  <Button
                    variant="outline"
                    size="sm"
                    className="border-indigo-500 text-indigo-700 hover:bg-indigo-50"
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
                  <Button
                    variant="destructive"
                    size="sm"
                    className="border-red-500 text-red-500 border-2 hover:bg-red-100"
                  >
                    Cancel
                  </Button>
                </div>
              </td>
            </tr>

            {/* Sample confirmed reservation */}
            <tr className="border-b border-gray-200 hover:bg-gray-50 transition-colors duration-150">
              <td className="p-4 font-medium text-gray-700">Jane Smith</td>
              <td className="p-4 text-gray-700">Hair Coloring</td>
              <td className="p-4 text-gray-700">2024-03-21</td>
              <td className="p-4 text-gray-700">10:30</td>
              <td className="p-4">
                <span className="px-3 py-1 rounded-full text-xs font-medium bg-emerald-100 text-emerald-800">
                  Confirmed
                </span>
              </td>
              <td className="p-4">
                <div className="flex space-x-2">
                  <Button
                    variant="outline"
                    size="sm"
                    className="border-indigo-500 bg-ind text-indigo-700 hover:bg-indigo-50"
                  >
                    Edit
                  </Button>
                  <Button
                    variant="destructive"
                    size="sm"
                    className="text-red-500 border-red-500 border-2 hover:bg-red-100"
                  >
                    Cancel
                  </Button>
                </div>
              </td>
            </tr>

            {/* Sample cancelled reservation */}
            <tr className="border-b border-gray-200 hover:bg-gray-50 transition-colors duration-150">
              <td className="p-4 font-medium text-gray-700">Robert Johnson</td>
              <td className="p-4 text-gray-700">Haircut</td>
              <td className="p-4 text-gray-700">2024-03-19</td>
              <td className="p-4 text-gray-700">16:15</td>
              <td className="p-4">
                <span className="px-3 py-1 rounded-full text-xs font-medium bg-gray-100 text-gray-800">
                  Cancelled
                </span>
              </td>
              <td className="p-4">
                <div className="flex space-x-2">
                  <Button
                    variant="outline"
                    size="sm"
                    className="border-indigo-500 text-indigo-700 hover:bg-indigo-50"
                  >
                    View
                  </Button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <div className="flex justify-between items-center pt-4">
        <p className="text-sm text-gray-500">Showing 3 of 3 reservations</p>
        <div className="flex space-x-2">
          <Button
            variant="outline"
            size="sm"
            className="border-gray-200 text-gray-700 hover:bg-gray-50 disabled:opacity-50"
            disabled
          >
            Previous
          </Button>
          <Button
            variant="outline"
            size="sm"
            className="border-gray-200 text-gray-700 hover:bg-gray-50 disabled:opacity-50"
            disabled
          >
            Next
          </Button>
        </div>
      </div>
    </div>
  );
}
