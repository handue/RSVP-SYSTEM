// src/components/reservation/StoreSelection.tsx
import { Store } from "../../../types/store";
interface StoreSelectionProps {
  onSelect: (store: Store) => void;
}

export const StoreSelection = ({ onSelect }: StoreSelectionProps) => {
  // Mock stores data
  const stores: Store[] = [
    {
      id: "store-1",
      name: "Hair Salon A",
      location: "Los Angeles",
      storeEmail: "hairsalon@example.com",
    },
    {
      id: "store-2",
      name: "Hair Salon B",
      location: "Texas",
      storeEmail: "hairsalon@example.com",
    },
    {
      id: "store-3",
      name: "Hair Salon C",
      location: "New York",
      storeEmail: "hairsalon@example.com",
    },
  ];

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
      {stores.map((store) => (
        <div
          key={store.id}
          className="p-4 border rounded-lg cursor-pointer hover:border-blue-500"
          onClick={() => onSelect(store)}
        >
          <h3 className="text-lg font-semibold">{store.name}</h3>
          <p className="text-gray-600">{store.location}</p>
        </div>
      ))}
    </div>
  );
};
