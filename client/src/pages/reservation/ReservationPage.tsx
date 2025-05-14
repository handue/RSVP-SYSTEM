import { useState } from "react";
import { Steps } from "../../components/ui/common/steps";
import { StoreSelection } from "../../components/ui/reservation/StoreSelection";
import { ServiceSelection } from "../../components/ui/reservation/ServiceSelection";
import { ReservationForm } from "../../components/ui/reservation/ReservationForm";
import { Store } from "../../types/store";
import { Service } from "../../types/service";

export const ReservationPage = () => {
  const [step, setStep] = useState<"store" | "service" | "details">("store");
  const [selectedStore, setSelectedStore] = useState<Store | null>(null);
  const [selectedService, setSelectedService] = useState<Service | null>(null);

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Make a Reservation</h1>

      <div className="mb-8">
        <Steps
          steps={[
            {
              title: "Store",
              status: step === "store" ? "current" : "complete",
            },
            {
              title: "Service",
              status: step === "service" ? "current" : "upcoming",
            },
            {
              title: "Details",
              status: step === "details" ? "current" : "upcoming",
            },
          ]}
        />
      </div>

      {step === "store" && (
        <StoreSelection
          onSelect={(store) => {
            setSelectedStore(store);
            setStep("service");
          }}
        />
      )}

      {step === "service" && selectedStore && (
        <ServiceSelection
          storeId={selectedStore.id}
          onSelect={(service) => {
            setSelectedService(service);
            setStep("details");
          }}
          onBack={() => setStep("store")}
        />
      )}

      {step === "details" && selectedStore && selectedService && (
        <ReservationForm
          storeId={selectedStore.id}
          storeName={selectedStore.name}
          storeEmail={selectedStore.storeEmail}
          serviceId={selectedService.id}
          serviceName={selectedService.name}
          onBack={() => setStep("service")}
        />
      )}
    </div>
  );
};
