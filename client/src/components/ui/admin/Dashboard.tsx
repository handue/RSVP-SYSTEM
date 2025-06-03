// src/components/ui/admin/Dashboard.tsx
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { Button } from "../common/button";
import { Card, CardContent, CardHeader, CardTitle } from "../common/card";
import { AppDispatch } from "../../../store";
import { SpecialDate, Store } from "../../../types/store";
import { useAppSelector } from "../../../hooks/useAppSelector";
import { useStoreHours } from "../../../hooks/useStoreHours";
import { Loading } from "../common/Loading";
import { useNotification } from "../../../hooks/useNotification";
import { saveStoreHours } from "../../../store/storeHoursSlice";

export const Dashboard = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { stores, status, error } = useAppSelector((state) => state.storeHours);
  const { getStoreHours } = useStoreHours();
  const { showError, showSuccess } = useNotification();

  const days = [0, 1, 2, 3, 4, 5, 6]; // Sunday to Saturday
  const dayNames = [
    "Sunday", "Monday", "Tuesday", "Wednesday", 
    "Thursday", "Friday", "Saturday"
  ];

  // 단일 상태로 통합 - 현재 편집 중인 store 데이터
  const [editingStore, setEditingStore] = useState<Store | null>(null);
  // 임시 특별 날짜 입력용 상태
  const [tempSpecialDate, setTempSpecialDate] = useState<Omit<SpecialDate, 'id'>>({
    date: "",
    open: "09:00",
    close: "18:00",
    isClosed: false,
  });

  useEffect(() => {
    const fetchStoreHours = async () => {
      await getStoreHours();
    };
    fetchStoreHours();
  }, []);

  // 스토어 선택시 해당 스토어의 전체 데이터를 복사해서 편집 상태로 설정
  const handleStoreSelect = (store: Store) => {
    setEditingStore({
      ...store,
      storeHour: {
        ...store.storeHour,
        regularHours: store.storeHour.regularHours.map(hour => ({ ...hour })),
        specialDate: store.storeHour.specialDate?.map(date => ({ ...date })) || []
      }
    });
  };

  // 정규 시간 변경
  const handleRegularHoursChange = (
    day: number,
    field: "open" | "close",
    value: string
  ) => {
    if (!editingStore) return;

    setEditingStore(prev => ({
      ...prev!,
      storeHour: {
        ...prev!.storeHour,
        regularHours: prev!.storeHour.regularHours.map(hour =>
          hour.day === day ? { ...hour, [field]: value } : hour
        )
      }
    }));
  };

  // 휴무일 토글
  const handleIsClosedChange = (day: number, isClosed: boolean) => {
    if (!editingStore) return;

    setEditingStore(prev => ({
      ...prev!,
      storeHour: {
        ...prev!.storeHour,
        regularHours: prev!.storeHour.regularHours.map(hour =>
          hour.day === day ? { ...hour, isClosed } : hour
        )
      }
    }));
  };

  // 임시 특별 날짜 입력 핸들러
  const handleTempSpecialDateChange = (
    field: keyof typeof tempSpecialDate,
    value: string | boolean
  ) => {
    setTempSpecialDate(prev => ({
      ...prev,
      [field]: value
    }));
  };

  // 특별 날짜 추가
  const handleAddSpecialDate = () => {
    if (!editingStore) return;
    
    if (!tempSpecialDate.date) {
      showError("Please select a date");
      return;
    }

    if (!tempSpecialDate.isClosed && (!tempSpecialDate.open || !tempSpecialDate.close)) {
      showError("Please fill in open and close times");
      return;
    }

    const newSpecialDate: SpecialDate = {
      ...tempSpecialDate,
      id: Date.now() // 임시 ID (실제로는 백엔드에서 생성)
    };

    setEditingStore(prev => ({
      ...prev!,
      storeHour: {
        ...prev!.storeHour,
        specialDate: [...(prev!.storeHour.specialDate || []), newSpecialDate]
      }
    }));

    // 입력 폼 초기화
    setTempSpecialDate({
      date: "",
      open: "09:00",
      close: "18:00",
      isClosed: false,
    });
  };

  // 특별 날짜 삭제
  const handleDeleteSpecialDate = (index: number) => {
    if (!editingStore) return;

    setEditingStore(prev => ({
      ...prev!,
      storeHour: {
        ...prev!.storeHour,
        specialDate: prev!.storeHour.specialDate?.filter((_, i) => i !== index) || []
      }
    }));
  };

  // 저장
  const handleSave = async () => {
    if (!editingStore) return;

    try {
      // 여기서 실제 API 호출
      await dispatch(saveStoreHours([editingStore]));
      showSuccess("Store hours saved successfully");
    } catch (error) {
      showError("Failed to save store hours");
    }
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

      {/* Store Selection */}
      {stores.length > 0 && (
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
                    editingStore?.storeId === store.storeId
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

      {/* Regular Hours */}
      {editingStore && (
        <Card>
          <CardHeader>
            <CardTitle>Regular Hours</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {days.map((day) => {
                const dayHour = editingStore.storeHour.regularHours.find(h => h.day === day);
                return (
                  <div key={day} className="flex items-center gap-4">
                    <div className="w-32">
                      <span className="font-medium text-gray-700">
                        {dayNames[day]}
                      </span>
                    </div>

                    <div
                      className={`flex w-full items-center justify-evenly gap-2 ${
                        dayHour?.isClosed ? "opacity-50 pointer-events-none" : ""
                      }`}
                    >
                      <input
                        type="time"
                        value={dayHour?.open || ""}
                        onChange={(e) => handleRegularHoursChange(day, "open", e.target.value)}
                        disabled={dayHour?.isClosed}
                        className="border-2 flex-1 rounded-md p-2"
                      />
                      <span>to</span>
                      <input
                        type="time"
                        value={dayHour?.close || ""}
                        onChange={(e) => handleRegularHoursChange(day, "close", e.target.value)}
                        disabled={dayHour?.isClosed}
                        className="border flex-1 rounded-md p-2"
                      />
                    </div>

                    <div className="flex items-center gap-2">
                      <label className="relative inline-flex items-center cursor-pointer">
                        <input
                          type="checkbox"
                          checked={dayHour?.isClosed || false}
                          onChange={(e) => handleIsClosedChange(day, e.target.checked)}
                          className="sr-only peer"
                        />
                        <div className="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 rounded-full peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-blue-600"></div>
                        <span className="ml-2 text-sm text-gray-500">Closed</span>
                      </label>
                    </div>
                  </div>
                );
              })}
            </div>
          </CardContent>
        </Card>
      )}

      {/* Special Dates */}
      {editingStore && (
        <Card className="mt-6">
          <CardHeader>
            <CardTitle>Special Dates</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {/* Add Special Date Form */}
              <div className="flex items-center gap-4">
                <input
                  type="date"
                  className="border rounded-md p-2"
                  value={tempSpecialDate.date}
                  onChange={(e) => handleTempSpecialDateChange("date", e.target.value)}
                />
                <input
                  type="time"
                  className="border rounded-md p-2"
                  value={tempSpecialDate.open}
                  onChange={(e) => handleTempSpecialDateChange("open", e.target.value)}
                  disabled={tempSpecialDate.isClosed}
                />
                <input
                  type="time"
                  className="border rounded-md p-2"
                  value={tempSpecialDate.close}
                  onChange={(e) => handleTempSpecialDateChange("close", e.target.value)}
                  disabled={tempSpecialDate.isClosed}
                />
                <label className="relative inline-flex items-center cursor-pointer">
                  <input
                    type="checkbox"
                    checked={tempSpecialDate.isClosed}
                    onChange={(e) => handleTempSpecialDateChange("isClosed", e.target.checked)}
                    className="sr-only peer"
                  />
                  <div className="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-blue-300 rounded-full peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-blue-600"></div>
                  <span className="ml-2 text-sm text-gray-500">Closed</span>
                </label>
                <Button
                  variant="outline"
                  className="bg-gray-200 border-gray-200 border-2 hover:bg-gray-400 text-sm"
                  onClick={handleAddSpecialDate}
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
                  {editingStore.storeHour.specialDate?.map((date, index) => (
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
                          showSuccess("Special date deleted successfully");
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
              onClick={handleSave}
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