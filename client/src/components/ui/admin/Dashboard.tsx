// src/components/ui/admin/Dashboard.tsx
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "../common/button";
import { Card, CardContent, CardHeader, CardTitle } from "../common/card";
import { AppDispatch, RootState } from "../../../store";
import { SpecialDate, Store } from "../../../types/store";
import { StoreHour } from "../../../types/store";
import { stores } from "../../reservation/StoreSelection";
import { useAppSelector } from "../../../hooks/useAppSelector";

import { useStoreHours } from "../../../hooks/useStoreHours";
import { Loading } from "../common/Loading";
import { useNotification } from "../../../hooks/useNotification";


export const Dashboard = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { stores, status, error } = useAppSelector((state) => state.storeHours);

  const { getStoreHours, saveStoreHourHook } = useStoreHours();

  const { showError, showSuccess } = useNotification();

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
  const [specialDate, setSpecialDate] = useState<SpecialDate | null>(null);

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
  };

  const handleIsClosedChange = (day: number, isClosed: boolean) => {
    setStoreHours((prev) => {
      return prev.map((storeHour) =>
        storeHour.storeId === selectedStore?.storeId
          ? {
              ...storeHour,
              regularHours: storeHour.regularHours.map((hour) =>
                hour.day === day
                  ? {
                      ...hour,
                      isClosed,
                      // 닫힌 날은 시간을 00:00으로 리셋
                    }
                  : hour
              ),
            }
          : storeHour
      );
    });
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

  const handleAddSpecialDate = (
    field: keyof SpecialDate,
    value: string | boolean
  ) => {
    setSpecialDate((prev) => ({
      ...prev,
      [field]: value,
      // 기본값 설정
      date: field === "date" ? (value as string) : prev?.date || "",
      open: field === "open" ? (value as string) : prev?.open || "09:00",
      close: field === "close" ? (value as string) : prev?.close || "18:00",
      isClosed:
        field === "isClosed" ? (value as boolean) : prev?.isClosed || false,
    }));
  };

  const handleCreateSpecialDate = (specialDate: SpecialDate) => {
    if (!specialDate.date || !specialDate.open || !specialDate.close) {
      showError("Please fill in all fields");
      return;
    }
    setStoreHours((prev) =>
      prev.map((sh) =>
        sh.storeId === selectedStore?.storeId
          ? {
              ...sh,
              specialDate: [...(sh.specialDate || []), specialDate],
            }
          : sh
      )
    );
  };

  const handleDeleteSpecialDate = (id: number) => {
    const storeHour = storeHours.find(
      (sh) => sh.storeId === selectedStore?.storeId
    );
    if (!storeHour) return;

    storeHour.specialDate = storeHour.specialDate?.filter(
      (_, index) => index !== id
    );
    setStoreHours([...storeHours]);
  };

  const mergeStoreHoursWithStores = (): Store[] => {
    return stores.map((store) => {
      const updatedStoreHour = storeHours.find(
        (sh) => sh.storeId === store.storeId
      );
      if (updatedStoreHour) {
        return {
          ...store,
          storeHour: {
            ...store.storeHour,
            regularHours: updatedStoreHour.regularHours,
            specialDate: updatedStoreHour.specialDate,
          },
        };
      }
      return store;
    });
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
      {/* TODO: mobile responsive implement needed */}
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

                  <div
                    className={`flex w-full items-center justify-evenly gap-2 ${
                      storeHours.find(
                        (sh) => sh.storeId === selectedStore.storeId
                      )?.regularHours[index].isClosed
                        ? "opacity-50 pointer-events-none"
                        : ""
                    }`}
                  >
                    <input
                      type="time"
                      value={
                        storeHours.find(
                          (sh) => sh.storeId === selectedStore.storeId
                        )?.regularHours[index].open || ""
                      }
                      onChange={(e) =>
                        handleRegularHoursChange(day, "open", e.target.value)
                      }
                      disabled={
                        storeHours.find(
                          (sh) => sh.storeId === selectedStore.storeId
                        )?.regularHours[index].isClosed
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
                      disabled={
                        storeHours.find(
                          (sh) => sh.storeId === selectedStore.storeId
                        )?.regularHours[index].isClosed
                      }
                      className="border flex-1 rounded-md p-2"
                    />
                  </div>
                  {/* isClosed 토글 */}
                  <div className="flex items-center gap-2">
                    <label className="relative inline-flex items-center cursor-pointer">
                      <input
                        type="checkbox"
                        checked={
                          storeHours.find(
                            (sh) => sh.storeId === selectedStore.storeId
                          )?.regularHours[index].isClosed || false
                        }
                        onChange={(e) =>
                          handleIsClosedChange(day, e.target.checked)
                        }
                        className="sr-only peer"
                      />
                      <div className="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 rounded-full peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-blue-600"></div>
                      <span className="ml-2 text-sm text-gray-500">Closed</span>
                    </label>
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
                  value={specialDate?.date}
                  onChange={(e) => handleAddSpecialDate("date", e.target.value)}
                />
                <input
                  type="time"
                  className="border rounded-md p-2"
                  value={specialDate?.open}
                  onChange={(e) => handleAddSpecialDate("open", e.target.value)}
                />
                <input
                  type="time"
                  className="border rounded-md p-2"
                  value={specialDate?.close}
                  onChange={(e) =>
                    handleAddSpecialDate("close", e.target.value)
                  }
                />
                <label className="relative inline-flex items-center cursor-pointer">
                  <input
                    type="checkbox"
                    onChange={(e) =>
                      handleAddSpecialDate("isClosed", e.target.checked)
                    }
                    className="sr-only peer"
                  />
                  <div className="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 rounded-full peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-blue-600"></div>
                  <span className="ml-2 text-sm text-gray-500">Closed</span>
                </label>
                <Button
                  variant="outline"
                  className="bg-gray-200 border-gray-200 border-2 hover:bg-gray-400 text-sm"
                  onClick={() => {
                    if (!specialDate) {
                      showError("Please select a special date");
                      return;
                    }
                    handleCreateSpecialDate(specialDate);
                    setSpecialDate({
                      date: "",
                      open: "",
                      close: "",
                      isClosed: false,
                    });
                    // showSuccess("Special date created successfully");

                    return;
                  }}
                >
                  Add Date
                </Button>
              </div>

              {/* Special Dates List */}
              <div className="mt-4">
                <h3 className="text-lg font-semibold mb-2">
                  Registered Special Dates
                </h3>
                <div className="space-y-2">
                  {storeHours
                    .find((sh) => sh.storeId === selectedStore?.storeId)
                    ?.specialDate?.map((date, index) => (
                      <div
                        key={index}
                        className="flex items-center justify-between p-3 border rounded-lg"
                      >
                        <div className="flex items-center gap-2">
                          <span className="font-medium">{date.date}</span>
                          <span className="text-sm text-gray-500">
                            {date.isClosed
                              ? "Closed"
                              : `${date.open} - ${date.close}`}
                          </span>
                        </div>
                        <Button
                          variant="ghost"
                          className="text-red-500 hover:text-red-700"
                          onClick={() => {
                            handleDeleteSpecialDate(index);
                          }}
                        >
                          Delete
                        </Button>
                      </div>
                    ))}
                </div>
              </div>
            </div>
          </CardContent>
          <div className="m-6 flex justify-center items-center">
            <Button
              onClick={async () => {
                const updatedStores = mergeStoreHoursWithStores();
                await saveStoreHourHook(updatedStores);
              }}
              className="bg-indigo-600 hover:bg-indigo-700 text-white"
            >
              Save Store Hours
            </Button>
          </div>
        </Card>
      )}
    </div>
  );
};
