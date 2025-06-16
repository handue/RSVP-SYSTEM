import { useMemo, useState } from "react";
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
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

interface ReservationFormProps {
  storeId: Store["storeId"];
  storeName: Store["name"];
  storeEmail: Store["storeEmail"];
  serviceId: Service["serviceId"];
  serviceName: Service["name"];
  storeHours: Store["storeHour"];
  onSuccess?: (reservationId: number) => void;
  onBack?: () => void;
}

export function ReservationForm({
  storeId,
  storeName,
  storeEmail,
  serviceId,
  serviceName,
  storeHours,
  onBack,
  onSuccess,
}: ReservationFormProps) {
  const { makeReservation, status } = useReservation();

  //   Partial = make every field in type to optional type
  const [formData, setFormData] = useState<Partial<ReservationData>>({
    store_name: storeName,
    store_id: storeId,
    store_email: storeEmail,
    service_name: serviceName,
    service_id: serviceId,
    agreedToTerms: false,
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      console.log("formData 확인", JSON.stringify(formData, null, 2));
      const result = await makeReservation(formData as ReservationData);
      console.log("result check:", JSON.stringify(result, null, 2));
      onSuccess?.(result.id);
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

  const formatPhoneNumber = (value: string) => {
    const cleaned = value.replace(/\D/g, "");
    if (cleaned.length <= 3) return cleaned;
    if (cleaned.length <= 6)
      return `${cleaned.slice(0, 3)}-${cleaned.slice(3)}`;
    return `${cleaned.slice(0, 3)}-${cleaned.slice(3, 6)}-${cleaned.slice(
      6,
      10
    )}`;
  };

  const isDateAvailable = (date: Date) => {
    const selectedDate = new Date(
      date.getFullYear(),
      date.getMonth(),
      date.getDate()
    );
    const dayOfWeek = selectedDate.getDay();
    console.log("스페셜데이트 확인:" + JSON.stringify(storeHours, null, 2));

    const specialDate = storeHours.specialDate?.find((sd) => {
      const specialDateDate = new Date(sd.date);
      return (
        specialDateDate.getFullYear() === selectedDate.getFullYear() &&
        specialDateDate.getMonth() === selectedDate.getMonth() &&
        specialDateDate.getDate() === selectedDate.getDate()
      );
    });

    // const specialDate = storeHours.specialDate?.find((sd) => sd.date === date);

    if (specialDate) {
      return !specialDate.isClosed;
    }

    const regularHours = storeHours.regularHours.find(
      (rh) => rh.day === dayOfWeek
    );

    return regularHours && !regularHours.isClosed;
  };

  const getAvailableTimeSlots = useMemo(() => {
    if (!formData.reservation_date) return false;

    const selectedDate = new Date(formData.reservation_date);
    const dayOfWeek = selectedDate.getDay();

    const special = storeHours.specialDate?.find((sd) => {
      const d = new Date(sd.date);
      return (
        d.getFullYear() === selectedDate.getFullYear() &&
        d.getMonth() === selectedDate.getMonth() &&
        d.getDate() === selectedDate.getDate()
      );
    });

    let open = "";
    let close = "";

    if (special && !special.isClosed) {
      open = special.open;
      close = special.close;
    } else {
      const regular = storeHours.regularHours?.find(
        (rh) => rh.day === dayOfWeek
      );
      if (regular && !regular.isClosed) {
        open = regular.open;
        close = regular.close;
      }
    }

    if (!open || !close) return [];

    const slots: string[] = [];
    let [h, m] = open.split(":").map(Number);
    const [endH, endM] = close.split(":").map(Number);

    while (h < endH || (h === endH && m < endM)) {
      const time = `${h.toString().padStart(2, "0")}:${m
        .toString()
        .padStart(2, "0")}`;
      slots.push(time);
      m += 30;
      if (m >= 60) {
        h += 1;
        m = 0;
      }
    }

    return slots;
  }, [formData.reservation_date, storeHours]);

  const TimeSlotPicker = () => {
    const timeSlots = getAvailableTimeSlots;

    if (!timeSlots) {
      return (
        <div className="text-sm text-black-500 border p-2 rounded-md">
          Choose the date first.
        </div>
      );
    }

    if (timeSlots.length === 0) {
      return (
        <div className="text-sm text-gray-500">
          This date is not available for reservation.
        </div>
      );
    }

    return (
      <div className="grid grid-cols-4 gap-2">
        {timeSlots.map((time) => (
          <button
            key={time}
            type="button"
            onClick={() =>
              setFormData((prev) => ({ ...prev, reservation_time: time }))
            }
            className={`p-2 text-sm rounded-md transition-colors ${
              formData.reservation_time === time
                ? "bg-indigo-600 text-white"
                : "bg-gray-100 hover:bg-gray-200 text-gray-700"
            }`}
          >
            {time}
          </button>
        ))}
      </div>
    );
  };

  const handleDateChange = (date: Date | null) => {
    if (date) {
      const dateString = date.toISOString().split("T")[0];
      setFormData((prev) => ({
        ...prev,
        reservation_date: dateString,
        reservation_time: "",
      }));
    }
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
              value={formData.phone ? formatPhoneNumber(formData.phone) : ""}
              onChange={(e) => {
                const rawValue = e.target.value.replace(/\D/g, "");
                if (rawValue.length <= 10) {
                  handleInputChange({
                    target: {
                      name: "phone",
                      value: rawValue,
                    },
                  } as React.ChangeEvent<HTMLInputElement>);
                }
              }}
              required
              className="w-full border-gray-300 focus:ring-indigo-500 focus:border-indigo-500 rounded-md shadow-sm"
              placeholder="123-456-7890"
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
              <div className="relative">
                <DatePicker
                  selected={
                    formData.reservation_date
                      ? new Date(formData.reservation_date)
                      : null
                  }
                  onChange={handleDateChange}
                  filterDate={(date) => {
                    const available = isDateAvailable(date);

                    return available !== undefined ? available : false;
                  }}
                  minDate={new Date()}
                  dateFormat="yyyy-MM-dd"
                  className="!w-full focus:ring-indigo-500 focus:border-indigo-500 rounded-md shadow-sm p-2 border border-gray-200 text-sm"
                  placeholderText="Select a date"
                  wrapperClassName="!w-full [&>*]:!w-full [&_input]:!w-full"
                  required
                />

                <span className="absolute inset-y-0 right-0 pr-3 flex items-center pointer-events-none">
                  <i
                    className="lucid-calendar-icon h-5 w-5 text-gray-400"
                    aria-hidden="true"
                  ></i>
                </span>
              </div>
            </div>

            <div className="space-y-2">
              <label
                htmlFor="reservation_time"
                className="block text-sm font-medium text-gray-700"
              >
                Reservation Time
              </label>
              <TimeSlotPicker />
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
              className="w-full border-gray-300 focus:ring-indigo-500 focus:border-indigo-500 rounded-md shadow-sm p-4 border"
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
