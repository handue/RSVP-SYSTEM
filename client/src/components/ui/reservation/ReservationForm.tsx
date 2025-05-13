import { useState } from "react";
import { useReservation } from "../../../hooks/useReservation";
import { ReservationData } from "../../../types/reservation";
import { Button } from "../common/button";
import { Input } from "../common/input";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "../common/select";

interface ReservationFormProps {
  storeId: string;
  storeName: string;
  storeEmail: string;
  onSuccess?: () => void;
}

export function ReservationForm({ storeId, storeName, storeEmail, onSuccess }: ReservationFormProps) {
  const { makeReservation, status } = useReservation();
  const [formData, setFormData] = useState<Partial<ReservationData>>({
    
    store: storeName,
    store_id: storeId,
    store_email: storeEmail,
    agreedToTerms: false,
    isAdvertisement: false,
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
    <form onSubmit={handleSubmit} className="space-y-4">
      <div className="space-y-2">
        <label htmlFor="name" className="text-sm font-medium">
          Name
        </label>
        <Input
          id="name"
          name="name"
          value={formData.name || ""}
          onChange={handleInputChange}
          required
        />
      </div>

      <div className="space-y-2">
        <label htmlFor="email" className="text-sm font-medium">
          Email
        </label>
        <Input
          id="email"
          name="email"
          type="email"
          value={formData.email || ""}
          onChange={handleInputChange}
          required
        />
      </div>

      <div className="space-y-2">
        <label htmlFor="phone" className="text-sm font-medium">
          Phone Number
        </label>
        <Input
          id="phone"
          name="phone"
          type="tel"
          value={formData.phone || ""}
          onChange={handleInputChange}
          required
        />
      </div>

      <div className="space-y-2">
        <label htmlFor="service" className="text-sm font-medium">
          Service
        </label>
        <Select
          name="service"
          value={formData.service}
          onValueChange={(value) =>
            setFormData((prev) => ({ ...prev, service: value }))
          }
        >
          <SelectTrigger>
            <SelectValue placeholder="Select a service" />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="haircut">Haircut</SelectItem>
            <SelectItem value="coloring">Hair Coloring</SelectItem>
            <SelectItem value="styling">Hair Styling</SelectItem>
          </SelectContent>
        </Select>
      </div>

      <div className="space-y-2">
        <label htmlFor="reservation_date" className="text-sm font-medium">
          Date
        </label>
        <Input
          id="reservation_date"
          name="reservation_date"
          type="date"
          value={formData.reservation_date || ""}
          onChange={handleInputChange}
          required
        />
      </div>

      <div className="space-y-2">
        <label htmlFor="reservation_time" className="text-sm font-medium">
          Time
        </label>
        <Input
          id="reservation_time"
          name="reservation_time"
          type="time"
          value={formData.reservation_time || ""}
          onChange={handleInputChange}
          required
        />
      </div>

      <div className="space-y-2">
        <label htmlFor="comments" className="text-sm font-medium">
          Additional Requests
        </label>
        <Input
          id="comments"
          name="comments"
          value={formData.comments || ""}
          onChange={handleInputChange}
        />
      </div>

      <div className="space-y-2">
        <label className="flex items-center space-x-2">
          <input
            type="checkbox"
            name="agreedToTerms"
            checked={formData.agreedToTerms}
            onChange={handleInputChange}
            required
          />
          <span className="text-sm">I agree to the terms and conditions</span>
        </label>
      </div>

      <div className="space-y-2">
        <label className="flex items-center space-x-2">
          <input
            type="checkbox"
            name="isAdvertisement"
            checked={formData.isAdvertisement}
            onChange={handleInputChange}
          />
          <span className="text-sm">I agree to receive promotional information</span>
        </label>
      </div>

      <Button
        type="submit"
        disabled={status === "loading"}
        className="w-full"
      >
        {status === "loading" ? "Reserving..." : "Make Reservation"}
      </Button>
    </form>
  );
}