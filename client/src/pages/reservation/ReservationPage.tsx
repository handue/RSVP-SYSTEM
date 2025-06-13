import { useEffect, useState } from "react";
import { Steps } from "../../components/ui/common/steps";
import { StoreSelection } from "../../components/reservation/StoreSelection";
import { ServiceSelection } from "../../components/reservation/ServiceSelection";
import { ReservationForm } from "../../components/reservation/ReservationForm";
import { Store, StoreHour } from "../../types/store";
import { Service } from "../../types/service";
import { useNavigate } from "react-router-dom";
import { useAppDispatch } from "../../hooks/useAppDispatch";
import { useStoreHours } from "../../hooks/useStoreHours";
import { useAppSelector } from "../../hooks/useAppSelector";

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
  const [storeHours, setStoreHours] = useState<StoreHour>({
    regularHours: [],
    specialDate: [],
    storeId: "",
  });
  const navigate = useNavigate();
  const { getStoreHours } = useStoreHours();

  const { stores } = useAppSelector((state) => state.storeHours);

  useEffect(() => {
    const fetchStoreHours = async () => {
      await getStoreHours();
      const store = stores.find((s) => s.storeId === selectedStore?.storeId);
      if (store) {
        setStoreHours(store.storeHour);
      }
    };
    fetchStoreHours();
  }, [getStoreHours, selectedStore]);

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
          storeHours={storeHours}
          serviceId={selectedService.serviceId}
          serviceName={selectedService.name}
          onSuccess={(reservationId) => {
            navigate(`/reservations/${reservationId}`);
          }}
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
