import { useState } from "react";
import { Steps } from "../../components/ui/common/steps";
import { StoreSelection } from "../../components/reservation/StoreSelection";
import { ServiceSelection } from "../../components/reservation/ServiceSelection";
import { ReservationForm } from "../../components/reservation/ReservationForm";
import { Store } from "../../types/store";
import { Service } from "../../types/service";

export const ReservationPage = () => {
  const [step, setStep] = useState<"store" | "service" | "details">("store");
  const [completedSteps, setCompletedSteps] = useState<Record<string, boolean>>(
    {
      store: false,
      service: false,
      details: false,
    }
  );
  const [selectedStore, setSelectedStore] = useState<Store | null>(null);
  const [selectedService, setSelectedService] = useState<Service | null>(null);

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Make a Reservation</h1>

      <div className="mb-8 flex items-center">
        <Steps
          steps={[
            {
              title: "Store",
              status:
                step === "store"
                  ? "current"
                  : completedSteps.store
                  ? "complete"
                  : "upcoming",
            },
            {
              title: "Service",
              status:
                step === "service"
                  ? "current"
                  : completedSteps.service
                  ? "complete"
                  : "upcoming",
            },
            {
              title: "Details",
              status:
                step === "details"
                  ? "current"
                  : completedSteps.details
                  ? "complete"
                  : "upcoming",
            },
          ]}
        />
      </div>

      {step === "store" && (
        <StoreSelection
          onSelect={(store) => {
            setSelectedStore(store);
            setStep("service");
            setCompletedSteps({
              ...completedSteps,
              store: true,
            });
          }}
        />
      )}

      {step === "service" && selectedStore && (
        <ServiceSelection
          storeId={selectedStore.storeId}
          onSelect={(service) => {
            setSelectedService(service);
            setStep("details");
            setCompletedSteps({
              ...completedSteps,
              service: true,
            });
          }}
          onBack={() => {
            setStep("store");
            setCompletedSteps({
              ...completedSteps,
              store: false,
            });
          }}
        />
      )}

      {step === "details" && selectedStore && selectedService && (
        <ReservationForm
          storeId={selectedStore.storeId}
          storeName={selectedStore.name}
          storeEmail={selectedStore.storeEmail}
          serviceId={selectedService.serviceId}
          serviceName={selectedService.name}
          onBack={() => {
            setStep("service");
            setCompletedSteps({
              ...completedSteps,
              service: false,
            });
          }}
        />
      )}
    </div>
  );
};
