import { StoreSelection } from "../../components/reservation/StoreSelection";
import { ReservationList } from "../../components/reservation/ReservationList";
import { useState } from "react";
import { Store } from "../../types/store";

export const ReservationListPage = () => {
  // TODO: FETCH RESERVATION LIST DATA NEEDED
  //   TODO : 스토어 아이디 받아와서, 해당 스토어의 예약 목록을 보여주는중. 스토어 select 할 수 있게 해야할듯.

  // Mock store ID
  //   const storeId = "store-1";
  const [selectedStore, setSelectedStore] = useState<Store | null>(null);

  return (
    <div>
      <h1 className="text-2xl font-bold mb-6 text-center text-gray-800">
        Reservation Management
      </h1>

      <div className="bg-white rounded-lg shadow-md p-6 md:p-8 mb-6">
        <StoreSelection
          onSelect={(store) => {
            setSelectedStore(store);
          }}
        />
      </div>

      {selectedStore && (
        <div className="bg-white rounded-lg shadow-md p-6 md:p-8">
          <ReservationList
            storeId={selectedStore.id}
            storeName={selectedStore.name}
          />
        </div>
      )}
    </div>
  );
};
