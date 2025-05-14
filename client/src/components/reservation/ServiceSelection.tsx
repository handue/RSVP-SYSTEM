import { Button } from "../ui/common/button";
import { Service } from "../../types/service";
import { Store } from "../../types/store";

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
      <div className="flex items-center mb-6">
        <button 
          onClick={onBack}
          className="flex items-center text-indigo-600 hover:text-indigo-800 transition-colors duration-200 font-medium"
        >
          <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5 mr-1" viewBox="0 0 20 20" fill="currentColor">
            <path fillRule="evenodd" d="M9.707 16.707a1 1 0 01-1.414 0l-6-6a1 1 0 010-1.414l6-6a1 1 0 011.414 1.414L5.414 9H17a1 1 0 110 2H5.414l4.293 4.293a1 1 0 010 1.414z" clipRule="evenodd" />
          </svg>
          Back to Stores
        </button>
      </div>
      
      <h2 className="text-xl font-semibold mb-4 text-gray-800">Select a Service</h2>
      
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        {services.map((service) => (
          <div
            key={service.id}
            className="p-6 border border-gray-200 rounded-lg shadow-sm cursor-pointer 
                     hover:border-indigo-500 hover:shadow-md transition-all duration-200
                     bg-white"
            onClick={() => onSelect(service)}
          >
            <div className="flex justify-between items-start mb-3">
              <h3 className="text-lg font-bold text-gray-900">{service.name}</h3>
              <span className="font-bold text-indigo-600 text-lg">${service.price.toLocaleString()}</span>
            </div>
            
            <div className="flex items-center text-gray-600 mb-3">
              <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 mr-1" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
              </svg>
              <p>{service.duration} minutes</p>
            </div>
            
            {service.description && (
              <p className="text-sm text-gray-500 mt-2 border-t border-gray-100 pt-3">
                {service.description}
              </p>
            )}
            
            <div className="mt-4 flex justify-end">
              <span className="text-indigo-600 font-medium flex items-center text-sm">
                Select
                <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 ml-1" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
                </svg>
              </span>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};