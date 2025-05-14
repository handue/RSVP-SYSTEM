import { Button } from "../common/button";
import { Service } from "../../../types/service";
import { Store } from "../../../types/store";

interface ServiceSelectionProps {
  storeId: Store["id"];
  onSelect: (service: Service) => void;
  onBack: () => void;
}

export const ServiceSelection = ({
  storeId,
  onSelect,
  onBack,
}: ServiceSelectionProps) => {
  // Mock services data
  const services: Service[] = [
    { id: "service-1", name: "Haircut", duration: 30, price: 30 },
    { id: "service-2", name: "Hair Coloring", duration: 120, price: 80 },
  ];

  return (
    <div>
      <div className="mb-4">
        <Button variant="outline" onClick={onBack}>
          ← Back to Stores
        </Button>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        {services.map((service) => (
          <div
            key={service.id}
            className="p-4 border rounded-lg cursor-pointer hover:border-blue-500"
            onClick={() => onSelect(service)}
          >
            <h3 className="text-lg font-semibold">{service.name}</h3>
            <p className="text-gray-600">
              {service.duration} minutes • ${service.price.toLocaleString()}
            </p>
            {service.description && (
              <p className="text-sm text-gray-500 mt-2">
                {service.description}
              </p>
            )}
          </div>
        ))}
      </div>
    </div>
  );
};
