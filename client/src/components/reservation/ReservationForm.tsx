import { useState } from "react";
import { useReservation } from "../../hooks/useReservation";
import { ReservationData } from "../../types/reservation";
import { Button } from "../ui/common/button";
import { Input } from "../ui/common/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../ui/common/select";
import { Store } from "../../types/store";
import { Service } from "../../types/service";

interface ReservationFormProps {
  storeId: Store["id"];
  storeName: Store["name"];
  storeEmail: Store["storeEmail"];
  serviceId: Service["id"];
  serviceName: Service["name"];
  onSuccess?: () => void;
  onBack?: () => void;
}

export function ReservationForm({
  storeId,
  storeName,
  storeEmail,
  serviceId,
  serviceName,
  onBack,
  onSuccess,
}: ReservationFormProps) {
  const { makeReservation, status } = useReservation();
  //   Partial = make every field in type to optional type
  const [formData, setFormData] = useState<Partial<ReservationData>>({
    store: storeName,
    store_id: storeId,
    store_email: storeEmail,
    service: serviceName,
    service_id: serviceId,
    agreedToTerms: false,
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await makeReservation(formData as ReservationData);
      onSuccess?.();
    } catch (error) {
      console.error("Failed to submit reservation:", error);
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, type, checked } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  return (
    <div className="max-w-2xl mx-auto">
      {onBack && (
        <div className="mb-6">
          <button
            onClick={onBack}
            className="flex items-center text-indigo-600 hover:text-indigo-800 transition-colors duration-200 font-medium"
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
            Back to Services
          </button>
        </div>
      )}

      <div className="bg-white rounded-lg shadow-md border border-gray-200 p-8">
        <h2 className="text-xl font-semibold mb-6 text-gray-800">
          Complete Your Reservation
        </h2>

        <div className=" border border-indigo-300 rounded-lg p-4 mb-6">
          <div className="flex justify-between items-center">
            <div>
              <p className="text-sm text-gray-500">Selected Store</p>
              <p className="font-medium text-gray-900">{storeName}</p>
            </div>
            <div>
              <p className="text-sm text-gray-500">Selected Service</p>
              <p className="font-medium text-gray-900">{serviceName}</p>
            </div>
          </div>
        </div>

        <form onSubmit={handleSubmit} className="space-y-5">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-5">
            <div className="space-y-2">
              <label
                htmlFor="name"
                className="block text-sm font-medium text-gray-700"
              >
                Full Name
              </label>
              <Input
                id="name"
                name="name"
                value={formData.name || ""}
                onChange={handleInputChange}
                required
                className="w-full border-gray-300 focus:ring-indigo-500 focus:border-indigo-500 rounded-md shadow-sm"
                placeholder="Enter your full name"
              />
            </div>

            <div className="space-y-2">
              <label
                htmlFor="email"
                className="block text-sm font-medium text-gray-700"
              >
                Email Address
              </label>
              <Input
                id="email"
                name="email"
                type="email"
                value={formData.email || ""}
                onChange={handleInputChange}
                required
                className="w-full border-gray-300 focus:ring-indigo-500 focus:border-indigo-500 rounded-md shadow-sm"
                placeholder="you@example.com"
              />
            </div>
          </div>

          <div className="space-y-2">
            <label
              htmlFor="phone"
              className="block text-sm font-medium text-gray-700"
            >
              Phone Number
            </label>
            <Input
              id="phone"
              name="phone"
              type="tel"
              value={formData.phone || ""}
              onChange={handleInputChange}
              required
              className="w-full border-gray-300 focus:ring-indigo-500 focus:border-indigo-500 rounded-md shadow-sm"
              placeholder="(123) 456-7890"
            />
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-5">
            <div className="space-y-2">
              <label
                htmlFor="reservation_date"
                className="block text-sm font-medium text-gray-700"
              >
                Reservation Date
              </label>
              <Input
                id="reservation_date"
                name="reservation_date"
                type="date"
                value={formData.reservation_date || ""}
                onChange={handleInputChange}
                required
                className="w-full border-gray-300 focus:ring-indigo-500 focus:border-indigo-500 rounded-md shadow-sm"
              />
            </div>

            <div className="space-y-2">
              <label
                htmlFor="reservation_time"
                className="block text-sm font-medium text-gray-700"
              >
                Reservation Time
              </label>
              <Input
                id="reservation_time"
                name="reservation_time"
                type="time"
                value={formData.reservation_time || ""}
                onChange={handleInputChange}
                required
                className="w-full border-gray-300 focus:ring-indigo-500 focus:border-indigo-500 rounded-md shadow-sm"
              />
            </div>
          </div>

          <div className="space-y-2">
            <label
              htmlFor="comments"
              className="block text-sm font-medium text-gray-700"
            >
              Additional Requests or Notes
            </label>
            <textarea
              id="comments"
              name="comments"
              value={formData.comments || ""}
              onChange={(e) =>
                setFormData((prev) => ({ ...prev, comments: e.target.value }))
              }
              rows={3}
              className="w-full border-gray-300 focus:ring-indigo-500 focus:border-indigo-500 rounded-md shadow-sm p-4"
              placeholder="Any special requests or information we should know"
            />
          </div>

          <div className="pt-2">
            <label className="flex items-center">
              <input
                type="checkbox"
                name="agreedToTerms"
                checked={formData.agreedToTerms}
                onChange={handleInputChange}
                required
                className="h-4 w-4 text-indigo-600 focus:ring-indigo-500 border-gray-300 rounded"
              />
              <span className="ml-2 text-sm text-gray-600">
                I agree to the{" "}
                <a href="#" className="text-indigo-600 hover:text-indigo-800">
                  terms and conditions
                </a>
              </span>
            </label>
          </div>

          <div className="pt-4">
            <Button
              type="submit"
              disabled={status === "loading"}
              className="w-full bg-indigo-600 hover:bg-indigo-700 text-white font-medium py-2 px-4 rounded-md shadow-sm transition duration-200 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
            >
              {status === "loading" ? (
                <div className="flex items-center justify-center">
                  <svg
                    className="animate-spin -ml-1 mr-2 h-4 w-4 text-white"
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                  >
                    <circle
                      className="opacity-25"
                      cx="12"
                      cy="12"
                      r="10"
                      stroke="currentColor"
                      strokeWidth="4"
                    ></circle>
                    <path
                      className="opacity-75"
                      fill="currentColor"
                      d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"
                    ></path>
                  </svg>
                  Confirming Reservation...
                </div>
              ) : (
                "Complete Reservation"
              )}
            </Button>
          </div>
        </form>
      </div>
    </div>
  );
}
