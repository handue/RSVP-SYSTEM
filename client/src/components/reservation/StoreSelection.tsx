import { Store } from "../../types/store";

interface StoreSelectionProps {
  onSelect: (store: Store) => void;
}

export const stores: Partial<Store>[] = [
  {
    storeId: "store-1",
    name: "Hair Salon A",
    location: "Los Angeles",
    storeEmail: "hairsalon@example.com",
  },
  {
    storeId: "store-2",
    name: "Hair Salon B",
    location: "Texas",
    storeEmail: "hairsalon@example.com",
  },
  {
    storeId: "store-3",
    name: "Hair Salon C",
    location: "New York",
    storeEmail: "hairsalon@example.com",
  },
];

export const StoreSelection = ({ onSelect }: StoreSelectionProps) => {
  return (
    <div>
      <h2 className="text-xl font-semibold mb-4 text-gray-800">
        Select a Store
      </h2>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {stores.map((store) => (
          <div
            key={store.storeId}
            className="p-6 border border-gray-200 rounded-lg shadow-sm cursor-pointer 
                     hover:border-indigo-500 hover:shadow-md transition-all duration-200
                     bg-white flex flex-col justify-between"
            onClick={() => onSelect(store as Store)}
          >
            <div>
              <h3 className="text-lg font-bold text-gray-900 mb-1">
                {store.name}
              </h3>
              <div className="flex items-center text-gray-600 mb-3">
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
                    d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z"
                  />
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M15 11a3 3 0 11-6 0 3 3 0 016 0z"
                  />
                </svg>
                <p>{store.location}</p>
              </div>
            </div>
            <div className="mt-2">
              <span className="text-indigo-600 font-medium flex items-center text-sm">
                Select
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  className="h-4 w-4 ml-1"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke="currentColor"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M9 5l7 7-7 7"
                  />
                </svg>
              </span>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};
