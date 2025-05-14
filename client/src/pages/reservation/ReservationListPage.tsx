import { StoreSelection } from "../../components/ui/reservation/StoreSelection";
import { ReservationList } from "../../components/ui/reservation/ReservationList";
import { useState } from "react";
import { Store } from "../../types/store";

export const ReservationListPage = () => {
  // TODO: FETCH RESERVATION LIST DATA NEEDED
  //   TODO : 스토어 아이디 받아와서, 해당 스토어의 예약 목록을 보여주는중. 스토어 select 할 수 있게 해야할듯.

  // Mock store ID
//   const storeId = "store-1";
  const [selectedStore, setSelectedStore] = useState<Store | null>(null);

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Reservations</h1>
      <StoreSelection
        onSelect={(store) => {
          setSelectedStore(store);
        }}
      />
      {selectedStore && <ReservationList storeId={selectedStore.id} />}
    </div>
  );
};
