// src/components/ui/admin/Dashboard.tsx
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "../common/button";
import { Card, CardContent, CardHeader, CardTitle } from "../common/card";
import { AppDispatch, RootState } from "../../../store";
import { Store } from "../../../types/store";
import { StoreHour } from "../../../types/store";
import { stores } from "../../reservation/StoreSelection";
import { useAppSelector } from "../../../hooks/useAppSelector";

import { useStoreHours } from "../../../hooks/useStoreHours";
import { Loading } from "../common/Loading";

export const Dashboard = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { stores, status, error } = useAppSelector((state) => state.storeHours);

  const { getStoreHours } = useStoreHours();

  const days = [
    0, // * 0 = Sunday
    1, // * 1 = Monday
    2, // * 2 = Tuesday
    3, // * 3 = Wednesday
    4, // * 4 = Thursday
    5, // * 5 = Friday
    6, // * 6 = Saturday
  ];

  const dayNames = [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday",
  ];

  const [selectedStore, setSelectedStore] = useState<Store | null>(null);
  const [storeHours, setStoreHours] = useState<StoreHour[]>([]);
  //   stores.map((store) => ({
  //     storeId: store.storeId,
  //     regularHours: days.map((day) => ({
  //       day,
  //       open: "09:00",
  //       close: "18:00",
  //       isClosed: false,
  //     })),
  //   }
  // )
  // )

  // 요일별 기본 영업시간
  // todo: edit needed after backend integration.
  useEffect(() => {
    const fetchStoreHours = async () => {
      await getStoreHours();
    };
    fetchStoreHours();
  }, [dispatch]);

  useEffect(() => {
    // setStoreHours(selectedStore?.storeHours);

    if (stores.length > 0) {
      setStoreHours(
        stores.map((store, index) => ({
          id: store.id ? store.id : index,
          storeId: store.storeId,
          regularHours: days.map((day: number) => ({
            day,
            open:
              store.storeHour?.regularHours.find(
                (regularHour) => regularHour.day === day
              )?.open || "",
            close:
              store.storeHour?.regularHours.find(
                (regularHour) => regularHour.day === day
              )?.close || "",
            isClosed:
              store.storeHour?.regularHours.find(
                (regularHour) => regularHour.day === day
              )?.isClosed || false,
          })),
          specialDate: store.storeHour?.specialDate,
        }))
      );
    }
  }, [stores]);

  const handleStoreSelect = (store: Store) => {
    
    setSelectedStore(store);
    setStoreHours((prev) =>
      prev.map((storeHour) =>
        storeHour.storeId === store.storeId
          ? { ...storeHour, storeId: store.storeId }
          : storeHour
      )
    );
    // TODO: 해당 스토어의 영업시간 데이터 가져오기
    // todo: 지금은 맨 위 데이터로 하면 될거같음.
  };

  const handleRegularHoursChange = (
    day: number,
    field: "open" | "close",
    value: string
  ) => {
    setStoreHours((prev) => {
      return prev.map((storeHour) =>
        storeHour.storeId === selectedStore?.storeId
          ? {
              ...storeHour,
              regularHours: storeHour.regularHours.map((hour) =>
                hour.day === day ? { ...hour, [field]: value } : hour
              ),
            }
          : storeHour
      );
    });
  };

  const handleSpecialDate = (date: string, isClosed: boolean) => {
    // TODO: 특정 날짜 휴무 설정
  };

  if (status === "loading") {
    return <Loading />;
  }

  return (
    <div className="p-6 border-2 border-gray-200 shadow-lg rounded-md">
      <div className="mb-8">
        <h1 className="text-2xl font-bold text-gray-800">
          Store Hours Management
        </h1>
        <p className="text-gray-500 mt-1">
          Manage store operating hours and special dates
        </p>
      </div>

      {/* 스토어 선택 */}
      {storeHours.length > 0 && (
        <Card className="mb-6">
          <CardHeader>
            <CardTitle>Select Store</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
              {stores.map((store) => (
                <div
                  key={store.storeId}
                  className={`p-4 border rounded-lg cursor-pointer transition-colors ${
                    selectedStore?.storeId === store.storeId
                      ? "border-indigo-500 bg-indigo-50"
                      : "border-gray-200 hover:border-indigo-300"
                  }`}
                  onClick={() => handleStoreSelect(store)}
                >
                  <h3 className="font-medium text-gray-900">{store.name}</h3>
                  <p className="text-sm text-gray-500">{store.location}</p>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>
      )}

      {/* 영업시간 설정 */}
      {selectedStore && (
        <Card>
          <CardHeader>
            <CardTitle>Regular Hours</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {days.map((day, index) => (
                <div key={index} className="flex items-center gap-4">
                  <div className="w-32">
                    <span className="font-medium text-gray-700">
                      {dayNames[day]}
                    </span>
                  </div>
                  <div className="flex w-full items-center justify-evenly gap-2">
                    <input
                      type="time"
                      // ! storeId를 number로 하면 편하겠지만, String 으로 했을때의 유연성을 갖지 않아서, 확장을 위해 우선은 String 으로 ID 셋업하고 find 함수 사용
                      // ! If I set storeId as number, it would be more convenient, but I chose to set it as a string to maintain flexibility for future extensions.

                      value={
                        storeHours.find(
                          (sh) => sh.storeId === selectedStore.storeId
                        )?.regularHours[index].open || ""
                      }
                      onChange={(e) =>
                        handleRegularHoursChange(day, "open", e.target.value)
                      }
                      className="border-2 flex-1 rounded-md p-2"
                    />
                    <span>to</span>
                    <input
                      type="time"
                      value={
                        storeHours.find(
                          (sh) => sh.storeId === selectedStore.storeId
                        )?.regularHours[index].close || ""
                      }
                      onChange={(e) =>
                        handleRegularHoursChange(day, "close", e.target.value)
                      }
                      className="border flex-1 rounded-md p-2"
                    />
                  </div>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>
      )}

      {/* 특별 휴무일 설정 */}
      {selectedStore && (
        <Card className="mt-6">
          <CardHeader>
            <CardTitle>Special Dates</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              <div className="flex items-center gap-4">
                <input
                  type="date"
                  className="border rounded-md p-2"
                  onChange={(e) => handleSpecialDate(e.target.value, true)}
                />
                <Button
                  variant="outline"
                  className="bg-gray-200 border-gray-200 border-2 hover:bg-gray-400"
                >
                  Add Closed Date
                </Button>
              </div>
              {/* TODO: 특별 휴무일 목록 표시 */}
            </div>
          </CardContent>
        </Card>
      )}
    </div>
  );
};
